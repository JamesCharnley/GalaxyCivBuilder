using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Planet : HabitableObject, IResourceController, IBuildable
{

    // IBuildable interface implementation
    public Buildable BuildableControl { get; set; }

    //IResourceController interface implementation
    public ResourceController ResourceControl { get; set; }

    // Planet variables
    public List<Resource> AvailableResources { get; set; }
    public List<RawResource> AvailableRawResources { get; set; }

    public Planet(AvailableResourcesTemplate _availableResourcesTemplate, BuildableTemplate _buildableTemplate, DisplayInfo _displayInfo) : base()
    {

        // IResourceController init
        ResourceControl = new ResourceController();
        ResourceControl.Inputs = new List<ResourceInOut>();
        ResourceControl.Outputs = new List<ResourceInOut>();

        // IBuildable init
        BuildableControl = new Buildable();
        BuildableControl.CompatibleFacilities = _buildableTemplate.CompatibleFacilities;
        BuildableControl.CurrentSlots = _buildableTemplate.CurrentSlots;
        BuildableControl.MaxSlots = _buildableTemplate.MaxSlots;
        BuildableControl.BuildSlots = new List<FacilityData>();
        ResourceManager resourceManager = GameObject.FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            foreach (FacilityData fd in _buildableTemplate.BuildSlots)
            {
                FacilityData data;
                if (resourceManager.FacilityInfoDatabase.TryGetValue(fd.Facility, out data))
                {
                    BuildableControl.BuildSlots.Add(data);
                    ResourceControl.UpdateInputs(data.Inputs.ToList());
                    ResourceControl.UpdateOutputs(data.Outputs.ToList());
                    Debug.Log("Added data to buildslot");
                }
                else
                {
                    Debug.LogWarning("Failed to get facility from FacilityDB");
                }

            }
        }
        else
        {
            Debug.LogWarning("ResourceManager is null");
        }

        // Planet init
        AvailableResources = _availableResourcesTemplate.AvailableResources;
        AvailableRawResources = _availableResourcesTemplate.AvailableRawResources;
        MenuType = EMenuType.Planet;
        DisplayInformation = _displayInfo;
    }


}
