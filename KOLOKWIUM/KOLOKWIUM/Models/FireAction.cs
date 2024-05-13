using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KOLOKWIUM.Models;

public class FireAction
{
    [Required]
    public int IdFireAction { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    public DateTime Endtime { get; set; }
    [Required]
    public bool NeedSpecialEquipment { get; set; }
}