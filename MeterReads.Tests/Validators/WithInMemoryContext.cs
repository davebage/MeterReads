using MeterReads.Data;
using Microsoft.EntityFrameworkCore;

namespace MeterReads.Tests.Validators;

public abstract class WithInMemoryContext
{
    protected DbContextClass _context;

    protected WithInMemoryContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContextClass>();
        optionsBuilder.UseInMemoryDatabase("MeterReadInMemoryDb");
        _context = new DbContextClass(optionsBuilder.Options);
    }
}