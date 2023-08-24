using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRender : MonoBehaviour, IInteractableRender
{

    public Planet PlanetData { get; set; }
    public InteractableObject Interactable { get; set; }

    public List<ResourceInOut> Inputs = new List<ResourceInOut>();
    public List<ResourceInOut> Outputs = new List<ResourceInOut>();
    public List<EFacility> CompatibleFacilities = new List<EFacility>();
    public List<Resource> AvailableResources = new List<Resource>();
    // Start is called before the first frame update
    void Start()
    {
        Interactable = PlanetData;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVariables()
    {
        Inputs = PlanetData.ResourceControl.Inputs;
        Outputs = PlanetData.ResourceControl.Outputs;
        CompatibleFacilities = PlanetData.BuildableControl.CompatibleFacilities;
        AvailableResources = PlanetData.AvailableResourcesControl.Resources;
    }
}
