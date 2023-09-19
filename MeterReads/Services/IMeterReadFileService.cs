using MeterReads.Models;

namespace MeterReads.Services
{
    public interface IMeterReadFileService
    {
        Task<ProcessMeterReadFileResult> ProcessMeterReadFileAsync(IFormFile file);

        bool ValidateMeterRead(MeterReadModel meterRead);
    }
}
