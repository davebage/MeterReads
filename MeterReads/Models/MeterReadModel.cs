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
            new MeterRead
            {
                AccountId = AccountId,
                MeterReadValue = (uint)MeterReadValue,
                MeterReadDateTime = MeterReadingDateTime
            };

        public override string ToString() => 
            $"AccountId: {AccountId}, MeterReadDateTime: {MeterReadingDateTime}, MeterReadValue: {MeterReadValue}";
    }
}
