using MeterReads.Data;
using MeterReads.Models;

namespace MeterReads.Validators;

public class MeterReadIsNewerValidator : IMeterReadValidator
{
    private readonly DbContextClass _context;

    public MeterReadIsNewerValidator(DbContextClass context) =>
        _context = context;

    public bool Validate(MeterReadModel meterRead) =>
        !_context.MeterReads.Any(x =>
            x.AccountId == meterRead.AccountId && 
            x.MeterReadDateTime > meterRead.MeterReadingDateTime);
}