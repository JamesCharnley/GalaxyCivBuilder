using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemRender : MonoBehaviour, IInteractableRender
{
    StarSystem StarSystemData = null;
    public InteractableObject Interactable { get; set; }

    private void Start()
    {
        Interactable = StarSystemData;
    }

    public void RenderStarSystem(StarSystem _starSystem)
    {
       
    }

    public void DeRenderStarSystem(StarSystem _starSystem)
    {

    }

    public void SetData(StarSystem _starSystem)
    {
        StarSystemData = _starSystem;
    }

    public void InteractAction()
    {
        StarSystemSpawner spawner = GameObject.FindObjectOfType<StarSystemSpawner>();
        if(spawner != null)
        {
            spawner.ActivateStarSystemView(StarSystemData);
        }
    }
    public StarSystem GetStarSystemData()
    {
        return StarSystemData;
    }
}
