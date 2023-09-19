using FluentAssertions;
using MeterReads.Entities;
using MeterReads.Models;
using MeterReads.Validators;

namespace MeterReads.Tests.Validators;

public class MeterReadAlreadyExistsValidatorShould : WithInMemoryContext
{
    IMeterReadValidator _validator;
    MeterReadModel _model;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var meterRead = new MeterRead
        {
            AccountId = 1,
            MeterReadValue = 100,
            MeterReadDateTime = new DateTime(2023, 1, 1, 12, 12, 0)
        };
        _context.MeterReads.Add(meterRead);
        await _context.SaveChangesAsync();
        _validator = new MeterReadAlreadyExistsValidator(_context);
    }

    [SetUp]
    public void SetUp()
    {
        _model = new MeterReadModel
        {
            AccountId = 1,
            MeterReadValue = 100,
            MeterReadingDateTime = new DateTime(2023, 1, 1, 12, 12, 0)
        };
    }

    [Test]
    public void RejectWhenMeterReadInRepositoryThatMatchesOnAllProperties()
    {
        var result = _validator.Validate(_model);

        result.Should().BeFalse();
    }

    [Test]
    public void AcceptWhenMeterReadInRepositoryDoesNotMatchOnAccountId()
    {
        _model.AccountId = 2;
        var result = _validator.Validate(_model);

        result.Should().BeTrue();
    }

    [Test]
    public void AcceptWhenMeterReadInRepositoryDoesNotMatchOnMeterReadValue()
    {
        _model.MeterReadValue = 1000;
        var result = _validator.Validate(_model);

        result.Should().BeTrue();
    }
    [Test]
    public void AcceptWhenMeterReadInRepositoryDoesNotMatchOnMeterReadDate()
    {
        _model.MeterReadingDateTime = new DateTime(2023, 1, 1, 12, 20, 0);
        var result = _validator.Validate(_model);

        result.Should().BeTrue();
    }
}