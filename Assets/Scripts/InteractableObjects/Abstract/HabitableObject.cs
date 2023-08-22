using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HabitatStats
{
    public int Oxygen;
}
public abstract class HabitableObject : InteractableObject, IHabitat
{
    public Habitat HabitatControl { get; set; }

    public HabitableObject()
    {
        HabitatControl = new Habitat();
    }
}
