using System.ComponentModel.DataAnnotations;

namespace CRMBOT.Database.Models;

public class Company : DatabaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string ContactInformation { get; set; }

    [Required]
    public long INN { get; set; }
}