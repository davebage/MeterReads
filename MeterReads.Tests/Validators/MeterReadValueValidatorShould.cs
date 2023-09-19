using FluentAssertions;
using MeterReads.Models;
using MeterReads.Validators;

namespace MeterReads.Tests.Validators;

[TestFixture]
public class MeterReadValueValidatorShould
{
    private readonly MeterReadValueValidator _validator = new MeterReadValueValidator();

    [Test]
    public void ReturnFalseWhenValueLessThanZero()
    {
        var result = _validator.Validate(new MeterReadModel { MeterReadValue = -1 });

        result.Should().BeFalse();
    }

    [Test]
    public void ReturnFalseWhenValueIsSixOrMoreDigits()
    {
        var result = _validator.Validate(new MeterReadModel { MeterReadValue = 100000 });

        result.Should().BeFalse();
    }

    [Test]
    public void ReturnTrueWhenValueIsZero()
    {
        var result = _validator.Validate(new MeterReadModel { MeterReadValue = 0 });

        result.Should().BeTrue();
    }

    [Test]
    public void ReturnTrueWhenValueIs99999()
    {
        var result = _validator.Validate(new MeterReadModel { MeterReadValue = 99999 });

        result.Should().BeTrue();
    }
}