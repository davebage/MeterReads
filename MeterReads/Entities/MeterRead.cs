using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReads.Entities
{
    public class MeterRead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public DateTime MeterReadDateTime { get; set; }

        public uint MeterReadValue { get; set; }
    }
}
