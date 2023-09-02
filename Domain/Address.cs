using Domain.Common;

namespace Domain;

public class Address : BaseDomainEntity
{
    public string UniqueName { get; set; }
    public string Subcity { get; set; }
    public double Longtude { get; set; }
    public double Latitude { get; set; }
    public string FarmId { get; set; }
    public Farm Farm { get; set; }
}
