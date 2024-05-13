using System.ComponentModel.DataAnnotations;

namespace KOLOKWIUM.Models;

public class FireTruck
{
    [Required]
    public int IdFireTruck { get; set; }
    [Required]
    public string OperationNum { get; set; }
    [Required]
    public bool SpecialEquipment { get; set; }
}