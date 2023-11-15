using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : InteractableObject
{
    public string Name = "";
    public List<InteractableObject> Contents = new List<InteractableObject>();
    public List<StarSystemConnection> Connections = new List<StarSystemConnection>();
    public StarSystemRender StarSystemRender = null;

    public void AddConnection(StarSystemConnection _starSystemConnection)
    {
        Connections.Add(_starSystemConnection);
    }
}
