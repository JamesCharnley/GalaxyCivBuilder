using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : HabitableObject, IResourceController, IBuildable
{

    // IBuildable interface implementation
    public Buildable BuildableFunctions { get; set; }
    public int MaxSlots { get; set; }
    public int CurrentSlots { get; set; }
    public List<EFacility> CompatibleFacilities { get; set; }

    //IResourceController interface implementation
    public ResourceController ResourceControlFunctions { get; set; }
    public List<ResourceInOut> Inputs { get; set; }
    public List<ResourceInOut> Outputs { get; set; }

    public Planet(ResourceControllerTemplate _resourceControllerTemplate, BuildableTemplate _buildableTemplate)
    {
        // IBuildable init
        BuildableFunctions = new Buildable();
        BuildableFunctions.OwnerInterface = this;
        CompatibleFacilities = _buildableTemplate.CompatibleFacilities;

        // IResourceController init
        ResourceControlFunctions = new ResourceController();
        ResourceControlFunctions.OwnerInterface = this;
        Inputs = _resourceControllerTemplate.Inputs;
        Outputs = _resourceControllerTemplate.Outputs;
    }


}
