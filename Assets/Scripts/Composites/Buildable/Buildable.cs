using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFacility
{
    SolarFarm,
    FoodFarm
}
public class Buildable : MonoBehaviour
{
    public IBuildable OwnerInterface;
}
