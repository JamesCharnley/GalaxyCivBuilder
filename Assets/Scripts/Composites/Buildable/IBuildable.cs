using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    public Buildable BuildableFunctions { get; set; }
    public int MaxSlots { get; set; }
    public int CurrentSlots { get; set; }
    public List<EFacility> CompatibleFacilities { get; set; }
}
