using MeterReads.Data;
using MeterReads.Models;

namespace MeterReads.Validators;

public class MeterReadAlreadyExistsValidator : IMeterReadValidator
{
    readonly DbContextClass _context;

    public MeterReadAlreadyExistsValidator(DbContextClass context) => _context = context;

    public bool Validate(MeterReadModel meterRead) =>
        !_context.MeterReads.Any(x => x.AccountId == meterRead.AccountId &&
                                      x.MeterReadValue == meterRead.MeterReadValue &&
                                      x.MeterReadDateTime == meterRead.MeterReadingDateTime);
}