using MeterReads.Models;

namespace MeterReads.Validators;

public class MeterReadValueValidator : IMeterReadValidator
{
    const int MinValue = 0;
    const int MaxValue = 99999;

    public bool Validate(MeterReadModel meterRead) => 
        meterRead.MeterReadValue is >= MinValue and <= MaxValue;
}