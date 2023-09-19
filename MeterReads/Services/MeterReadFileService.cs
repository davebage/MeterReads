using MeterReads.Data;
using MeterReads.Models;
using MeterReads.Validators;

namespace MeterReads.Services;

public class MeterReadFileService : IMeterReadFileService
{
    readonly DbContextClass _context;
    //private ICSVService _csvService;
    readonly IEnumerable<IMeterReadValidator> _validators;
    public MeterReadFileService(
        DbContextClass context, 
        //ICSVService csvService, 
        IEnumerable<IMeterReadValidator> validators)
    {
        this._context = context;
        //_csvService = csvService;
        this._validators = validators;
    }

    public async Task<ProcessMeterReadFileResult> ProcessMeterReadFileAsync(IFormFile fileData)
    {
        var csvService = new CsvService();
        var result = new ProcessMeterReadFileResult();
        foreach (var meterRead in csvService.ReadCSV<MeterReadModel>(fileData.OpenReadStream()))
        {
            if (ValidateMeterRead(meterRead))
            {
                _context.MeterReads.Add(meterRead.ToEntity());
                await _context.SaveChangesAsync();
                result.Valid++;
            }
            else
            {
                result.Invalid++;
            }
        }
        return result;
    }

    public bool ValidateMeterRead(MeterReadModel meterRead)
    {
        foreach (var meterReadValidator in _validators)
        {
            if (!meterReadValidator.Validate(meterRead))
            {
                return false;
            }
        }
        return true;
    }
}