using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;


public class Planet : HabitableObject, IResourceController, IBuildable, ITransportController
{

    // IBuildable interface implementation
    public Buildable BuildableControl { get; set; }

    //IResourceController interface implementation
    public ResourceController ResourceControl { get; set; }
    public TransportController TransportControl { get; set; }

    public Planet(ResourceControllerTemplate _resourceControllerTemplate, BuildableTemplate _buildableTemplate, DisplayInfo _displayInfo) : base()
    {

        // IResourceController init
        ResourceControl = new ResourceController(new List<Resource>(), new List<Resource>(), new List<Resource>(), _resourceControllerTemplate.BaseResources, this);

        // IBuildable init
        BuildableControl = new Buildable(_buildableTemplate.MaxSlots, _buildableTemplate.CurrentSlots, _buildableTemplate.CompatibleFacilities, new List<FacilityData>(), this);

        // ITransportController init
        TransportControl = new();

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
                    ResourceControl.UpdateOccupiedResources(data.RequiredBaseResources.ToList());
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
        MenuType = EMenuType.Planet;
        DisplayInformation = _displayInfo;
    }


}
