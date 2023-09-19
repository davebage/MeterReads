using CsvHelper.Configuration.Attributes;
using MeterReads.Entities;

namespace MeterReads.Models
{
    public class MeterReadModel
    {
        [Name("AccountId")]
        public int AccountId { get; set; }

        [Name("MeterReadingDateTime")]
        public DateTime MeterReadingDateTime { get; set; }

        [Name("MeterReadValue")]
        public int MeterReadValue { get; set; }

        public MeterRead ToEntity() =>
            new()
            {
                AccountId = AccountId,
                MeterReadValue = (uint)MeterReadValue,
                MeterReadDateTime = MeterReadingDateTime
            };
    }
}
