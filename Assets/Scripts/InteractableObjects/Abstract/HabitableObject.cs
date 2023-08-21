using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HabitatStats
{
    public int Oxygen;
}
public abstract class HabitableObject : InteractableObject, IHabitat
{
    public Habitat HabitatFunctions { get; set; }
    public int Population { get; set; }
    public HabitatStats Stats { get; set; }
}
