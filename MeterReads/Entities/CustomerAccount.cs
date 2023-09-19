using System.ComponentModel.DataAnnotations;

namespace MeterReads.Entities;

public class CustomerAccount
{
    [Key] 
    public uint AccountId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}