using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : HabitableObject, IResourceController, IBuildable
{

    // IBuildable interface implementation
    public Buildable BuildableControl { get; set; }

    //IResourceController interface implementation
    public ResourceController ResourceControl { get; set; }

    // Planet variables
    public List<Resource> AvailableResources { get; set; }

    public Planet(AvailableResourcesTemplate _availableResourcesTemplate, BuildableTemplate _buildableTemplate)
    {
        // IBuildable init
        BuildableControl = new Buildable();
        BuildableControl.CompatibleFacilities = _buildableTemplate.CompatibleFacilities;

        // IResourceController init
        ResourceControl = new ResourceController();
        ResourceControl.Inputs = new List<ResourceInOut>();
        ResourceControl.Outputs = new List<ResourceInOut>();

        // Planet init
        AvailableResources = _availableResourcesTemplate.AvailableResources;
    }


}
