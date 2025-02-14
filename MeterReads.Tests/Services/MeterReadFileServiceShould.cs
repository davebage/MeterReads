using FluentAssertions;
using MeterReads.Entities;
using MeterReads.Models;
using MeterReads.Services;
using MeterReads.Tests.Validators;
using MeterReads.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MeterReads.Tests.Services
{
    [TestFixture]
    public class MeterReadFileServiceShould : WithInMemoryContext
    {
        IMeterReadFileService _service;

        [OneTimeSetUp]
        public void OneTimeSetUp() =>
            _service = new MeterReadFileService(
                _context,
                GetValidators());

        [TearDown]
        public void TearDown()
        {
            _context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);

            _context.Database.EnsureDeleted();
        }

        IEnumerable<IMeterReadValidator> GetValidators() =>
            new List<IMeterReadValidator>()
            {
                new MeterReadValueValidator(),
                new MeterReadAlreadyExistsValidator(_context),
                new MeterReadHasValidCustomerAccountValidator(_context),
                new MeterReadIsNewerValidator(_context)
            };

        [Test]
        public async Task ZeroValidAndTwoInvalid()
        {
            var result = await _service.ProcessMeterReadFileAsync(CreateMockFormFile());

            ValidateServiceResult(result,0,2);;
        }

        [Test]
        public async Task TwoValidAndZeroInvalid()
        {
            _context.CustomerAccounts.Add(new CustomerAccount { AccountId = 2233, FirstName = "", LastName = "" });
            _context.CustomerAccounts.Add(new CustomerAccount { AccountId = 2344, FirstName = "", LastName = "" });
            await _context.SaveChangesAsync();
            
            var result = await _service.ProcessMeterReadFileAsync(CreateMockFormFile());
            
            ValidateServiceResult(result,2,0);;
        }

        [Test]
        public async Task ReturnOneValidBecauseOfValidAccountIdAndOneInvalidBecauseOfMissingAccountId()
        {
            _context.CustomerAccounts.Add(new CustomerAccount { AccountId = 2344, FirstName = "", LastName = "" });
            await _context.SaveChangesAsync();
            var result = await _service.ProcessMeterReadFileAsync(CreateMockFormFile());
            ValidateServiceResult(result, 1, 1);;
        }

        static void ValidateServiceResult(ProcessMeterReadFileResult result, int expectedValid, int expectedInvalid)
        {
            var readResult = result.Should().BeOfType<ProcessMeterReadFileResult>().Subject;
            readResult.Valid.Should().Be(expectedValid);
            readResult.Invalid.Should().Be(expectedInvalid);
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
