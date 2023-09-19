using MeterReads.Models;

namespace MeterReads.Validators;

public class MeterReadValueValidator : IMeterReadValidator
{
    private const int MinValue = 0;
    private const int MaxValue = 99999;
    public bool Validate(MeterReadModel meterRead) => 
        meterRead.MeterReadValue is >= MinValue and <= MaxValue;
}