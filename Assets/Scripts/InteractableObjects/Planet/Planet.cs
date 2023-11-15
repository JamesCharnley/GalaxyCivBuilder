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
    
    //ITransportController interface implementation
    public TransportController TransportControl { get; set; }

    public Planet(ResourceControllerTemplate _resourceControllerTemplate, BuildableTemplate _buildableTemplate, DisplayInfo _displayInfo, FRenderInfo _renderInfo) : base()
    {

        // IResourceController init
        ResourceControl = new ResourceController(new List<Resource>(), new List<Resource>(), new List<Resource>(), _resourceControllerTemplate.BaseResources, this);

        // IBuildable init
        BuildableControl = new Buildable(_buildableTemplate.MaxSlots, _buildableTemplate.CurrentSlots, _buildableTemplate.CompatibleFacilities, new List<FacilityData>(), this);

        // ITransportController init
        TransportControl = new();

        // Planet init
        MenuType = EMenuType.Planet;
        DisplayInformation = _displayInfo;
        RenderInfo = _renderInfo;

        // Build planet with testing template
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
                }
            }
        }
    }


}
