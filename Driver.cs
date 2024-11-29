using System.ComponentModel.DataAnnotations;

public class Driver
{
    [Key]
    public int DriverId { get; set; }
    public required string Name { get; set; }
    public string IsAvailable { get; set; } = "YES";
    public int KilometersDriven { get; set; } = 0; 
}
