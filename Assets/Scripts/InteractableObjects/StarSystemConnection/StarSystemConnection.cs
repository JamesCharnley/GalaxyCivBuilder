using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemConnection
{
    public StarSystem StarSystemA = null;
    public StarSystem StarSystemB = null;
    public bool IsInitialized = false;

    public StarSystemConnection(StarSystem _starSystemA, StarSystem _starSystemB)
    {
        StarSystemA = _starSystemA;
        StarSystemB = _starSystemB;
    }
}
