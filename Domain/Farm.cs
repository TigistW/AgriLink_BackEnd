using Domain.Common;

namespace Domain;
public enum FarmingMethod
{
    CropRotation,
    None
}

public enum FarmingEquipment
{
    Tractor,
    Cattle
}

public enum WaterSource
{
    Tractor,
    Cattle
}
public class Farm :BaseDomainEntity
{
    public string UniqueName { get; set; }
    public string FarmName { get; set;}
    public string FarmDescription { get;}
    public string FarmId { get;}
    public string cropType { get; set; }

    public FarmingMethod farmingMethod{ get; set; }
    public FarmingEquipment farmingEquipment{ get; set; }
    public WaterSource waterSource{ get; set; }

    public Address Address { get; set; }
}
