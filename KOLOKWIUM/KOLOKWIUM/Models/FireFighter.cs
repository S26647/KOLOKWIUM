using System.ComponentModel.DataAnnotations;

namespace KOLOKWIUM.Models;

public class FireFighter
{
    [Required]
    public int IdFireFight { get; set; }
    [Required]
    [MaxLength(30)]
    public string FirstName { get;set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
}