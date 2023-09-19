using MeterReads.Models;

namespace MeterReads.Services
{
    public interface IMeterReadFileService
    {
        public Task<ProcessMeterReadFileResult> ProcessMeterReadFileAsync(IFormFile file);

        public bool ValidateMeterRead(MeterReadModel meterRead);
    }
}
