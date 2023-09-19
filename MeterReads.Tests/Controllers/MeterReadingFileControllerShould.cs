using Microsoft.AspNetCore.Http;
using FakeItEasy;
using FluentAssertions;
using MeterReads.Controllers;
using MeterReads.Models;
using MeterReads.Services;
using Microsoft.AspNetCore.Mvc;
using A = FakeItEasy.A;
namespace MeterReads.Tests.Controllers
{
    [TestFixture]
    public class MeterReadingFileControllerShould
    {
        [Test]
        public async Task ReturnOkResult()
        {
            var mockFormFile = CreateMockFormFile();
            var fileServiceFake = A.Fake<IMeterReadFileService>();

            var controller = new MeterReadingFileController(fileServiceFake);

            var result = await controller.PostSingleFile(new FileUploadModel { FileDetails = mockFormFile });
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            A.CallTo(() => fileServiceFake.ProcessMeterReadFileAsync(mockFormFile)).MustHaveHappened();
        }

        [Test]
        public async Task ReturnOkResultWithSingleValidAndSingleInvalid()
        {
            var mockFormFile = CreateMockFormFile();

            var fileServiceFake = A.Fake<IMeterReadFileService>();
            A.CallTo(() => fileServiceFake.ProcessMeterReadFileAsync(mockFormFile)).Returns(new ProcessMeterReadFileResult()
                { Valid = 1, Invalid = 1 });

            var controller = new MeterReadingFileController(fileServiceFake);

            var result = await controller.PostSingleFile(new FileUploadModel { FileDetails = mockFormFile });
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            A.CallTo(() => fileServiceFake.ProcessMeterReadFileAsync(mockFormFile)).MustHaveHappened();

            var processMeterReadResult = okResult.Value.Should().BeOfType<ProcessMeterReadFileResult>().Subject;
            processMeterReadResult.Invalid.Should().Be(1);
            processMeterReadResult.Valid.Should().Be(1);
        }

        [Test]
        public async Task ReturnStatusCodeResultOf500()
        {
            var mockFormFile = CreateMockFormFile();

            var fileServiceFake = A.Fake<IMeterReadFileService>();
            A.CallTo(() => fileServiceFake.ProcessMeterReadFileAsync(mockFormFile))
                .Throws<InvalidOperationException>();
            var controller = new MeterReadingFileController(fileServiceFake);

            var result = await controller.PostSingleFile(new FileUploadModel { FileDetails = mockFormFile });
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(500);
        }

        static IFormFile CreateMockFormFile()
        {
            const string csvContent = @"AccountId,MeterReadingDateTime,MeterReadValue,
2344,22/04/2019 09:24,1002,
2233,22/04/2019 12:25,323,
";
            var fileName = "testcsv.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(csvContent);
            writer.Flush();
            stream.Position = 0;

            return new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
        }

    }
}