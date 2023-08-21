using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFacility
{
    SolarFarm,
    FoodFarm
}
public class Buildable
{
    public int MaxSlots { get; set; }
    public int CurrentSlots { get; set; }
    public List<EFacility> CompatibleFacilities { get; set; }
}
