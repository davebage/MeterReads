using FluentAssertions;
using MeterReads.Entities;
using MeterReads.Models;
using MeterReads.Validators;

namespace MeterReads.Tests.Validators;

[TestFixture]
public class MeterReadHasValidCustomerAccountValidatorShould : WithInMemoryContext
{
    IMeterReadValidator _validator;
    MeterReadModel _meterReadModel;
    const int InvalidAccountId = 10;
    const int ValidAccountId = 1;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var customerAccount = new CustomerAccount { AccountId = ValidAccountId, FirstName = "Testy", LastName = "Testerson" };
        _context.CustomerAccounts.Add(customerAccount);
        await _context.SaveChangesAsync();
        _validator = new MeterReadHasValidCustomerAccountValidator(_context);
    }

    [SetUp]
    public void SetUp()
    {
        _meterReadModel = new MeterReadModel
        {
            AccountId = 1,
            MeterReadValue = 100, 
            MeterReadingDateTime = new DateTime(2023, 1, 1, 12, 12, 0)
        };
    }

    [Test]
    public void ValidateMeterReadWhenCustomerAccountExists()
    {
        var result = _validator.Validate(_meterReadModel);

        result.Should().BeTrue();
    }

    [Test]
    public void RejectMeterReadWhenCustomerAccountDoesNotExist()
    {
        _meterReadModel.AccountId = InvalidAccountId;
        var result = _validator.Validate(_meterReadModel);

        result.Should().BeFalse();
    }

}