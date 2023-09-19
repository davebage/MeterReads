using FluentAssertions;
using MeterReads.Entities;
using MeterReads.Models;
using MeterReads.Validators;

namespace MeterReads.Tests.Validators;

[TestFixture]
public class MeterReadDateIsNewerValidatorShould : WithInMemoryContext
{
    IMeterReadValidator _validator;
    MeterReadModel _meterReadModel;
    readonly DateTime savedMeterReadDateTime = new DateTime(2023, 1, 1, 12, 12, 0);
    readonly DateTime earlierMeterReadDateTime = new DateTime(2023, 1, 1, 10, 0, 0);
    readonly DateTime laterMeterReadDateTime = new DateTime(2023, 1, 2, 10, 0, 0);

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var meterRead = new MeterRead
        {
            AccountId = 1,
            MeterReadValue = 100,
            MeterReadDateTime = savedMeterReadDateTime
        };
        _context.MeterReads.Add(meterRead);
        await _context.SaveChangesAsync();
        _validator = new MeterReadIsNewerValidator(_context);
    }

    [SetUp]
    public void SetUp()
    {

        _meterReadModel = new MeterReadModel
        {
            AccountId = 1,
            MeterReadValue = 100,
            MeterReadingDateTime = earlierMeterReadDateTime
        };
    }

    [Test]
    public void RejectWhenMeterReadDateIsEarlierThanSavedMeterReadDate()
    {
        var result = _validator.Validate(_meterReadModel);

        result.Should().BeFalse();
    }

    [Test]
    public void AcceptWhenMeterReadDateIsLaterThanSavedMeterReadDate()
    {
        _meterReadModel.MeterReadingDateTime = laterMeterReadDateTime;
        var result = _validator.Validate(_meterReadModel);

        result.Should().BeTrue();
    }

    [Test]
    public void AcceptWhenMeterReadDateIsSameAsSavedMeterReadDate()
    {
        _meterReadModel.MeterReadingDateTime = savedMeterReadDateTime;
        var result = _validator.Validate(_meterReadModel);

        result.Should().BeTrue();
    }
}