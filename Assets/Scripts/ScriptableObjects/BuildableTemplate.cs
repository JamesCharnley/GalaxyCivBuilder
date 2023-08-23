using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "B_Template", menuName = "ScriptableObjects/Buildable")]
public class BuildableTemplate : ScriptableObject
{
    public List<EFacility> CompatibleFacilities;
    public List<FacilityData> BuildSlots;
    public int MaxSlots;
    public int CurrentSlots;
}
