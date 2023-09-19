using MeterReads.Data;
using MeterReads.Models;

namespace MeterReads.Validators;

public class MeterReadHasValidCustomerAccountValidator : IMeterReadValidator
{
    readonly DbContextClass _context;

    public MeterReadHasValidCustomerAccountValidator(DbContextClass context) => 
        _context = context;

    public bool Validate(MeterReadModel meterRead) => 
        _context.CustomerAccounts.Any(x => x.AccountId == meterRead.AccountId);
}