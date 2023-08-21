using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHabitat 
{
    public Habitat HabitatFunctions { get; set; }
    public int Population { get; set; }
    public HabitatStats Stats { get; set; }
}
