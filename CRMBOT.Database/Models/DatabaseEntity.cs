using System.ComponentModel.DataAnnotations.Schema;

namespace CRMBOT.Database.Models;

public class DatabaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
}