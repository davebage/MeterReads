using MeterReads.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeterReads.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MeterRead> MeterReads{ get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    }
}