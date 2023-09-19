using MeterReads.Models;

namespace MeterReads.Validators
{
    public interface IMeterReadValidator
    {
        public bool Validate(MeterReadModel meterRead);
    }
}