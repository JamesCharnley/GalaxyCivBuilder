using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public enum EFacility
{
    SolarFarm,
    FoodFarm,
    WindFarm,
    MetalMine,
    RareMetalMine,
    CarbonMine,
    SilicaMine,
    UraniumMine,
    WaterExtractor,
    NuclearPowerPlant
}
public class Buildable
{
    public int MaxSlots { get; set; }
    public int CurrentSlots { get; set; }
    public List<EFacility> CompatibleFacilities { get; set; }
    public List<FacilityData> BuildSlots { get; set; }

    IBuildable OwnerInterface { get; set; }

    public Buildable(int _maxSlots, int _currentSlots, List<EFacility> _compatibleFacilities, List<FacilityData> _buildSlots, IBuildable _ownerInterface) 
    {
        MaxSlots = _maxSlots;
        CurrentSlots = _currentSlots;
        CompatibleFacilities = _compatibleFacilities;
        BuildSlots = _buildSlots;
        OwnerInterface = _ownerInterface;
    }
    public bool CanBuild(FacilityData _facility)
    { 
        if(BuildSlots.Count >= CurrentSlots) return false;

        bool isCompatible = false;
        foreach(EFacility facility in CompatibleFacilities)
        {
            if(facility == _facility.Facility)
            {
                isCompatible = true;
                break;
            }
        }
        if (!isCompatible)
        {
            Debug.Log("Cannot build Facility: Facility is incompatible");
            return false;
        }

        //check if civilisation has enough resources to build the facility
        ResourceManager resManager = GameObject.FindObjectOfType<ResourceManager>();
        if (resManager == null)
        {
            Debug.Log("Cannot build Facility: ResoureceManager is null");
            return false;
        }

        foreach(Resource resCost in _facility.ResourceCost)
        {
            if(resCost.Amount > resManager.TotalResources[resCost.ResourceName])
            {
                Debug.Log("Cannot build Facility: Insufficant resources");
                return false;
            }
        }

        IResourceController resController = OwnerInterface as IResourceController;
        IAvailableResources availResources = OwnerInterface as IAvailableResources;

        if (availResources == null)
        {
            Debug.Log("Cannot build Facility: IAvailableResources is null");
            return false;
        }
        if (resController == null)
        {
            Debug.Log("Cannot build Facility: IResourceController is null");
            return false;
        }
        // Check if planet has enough required raw resources available
        if (_facility.RequiredRawResources.Length > 0)
        {
            foreach (RawResource reqRes in _facility.RequiredRawResources)
            {
                int reqResInUse = 0;
                foreach (RawResource rawRes in resController.ResourceControl.UsedRawResources)
                {
                    if (rawRes.ResourceName == reqRes.ResourceName)
                    {
                        reqResInUse = rawRes.Amount;
                        foreach (RawResource availRes in availResources.AvailableResourcesControl.RawResources)
                        {
                            if (availRes.ResourceName == reqRes.ResourceName)
                            {
                                if (reqResInUse + reqRes.Amount > availRes.Amount)
                                {
                                    Debug.Log("Cannot build Facility: Insufficant raw resources");
                                    return false;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        // Check if planet has enough normal resources available 
        if(_facility.Outputs.Length > 0)
        {
            foreach(Resource outRes in _facility.Outputs)
            {
                foreach(Resource availRes in availResources.AvailableResourcesControl.Resources)
                {
                    if(outRes.ResourceName == availRes.ResourceName)
                    {
                        foreach(ResourceInOut output in resController.ResourceControl.Outputs)
                        {
                            if(output.Resource == availRes.ResourceName)
                            {
                                if(outRes.Amount + output.CurrentAmount > availRes.Amount)
                                {
                                    Debug.Log("Cannot build Facility: Insufficant normal resources");
                                    return false;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        return true;
    }
    public void BuildFacility(FacilityData _facility)
    {
        IResourceController resController = OwnerInterface as IResourceController;
        if(resController != null)
        {
            if(_facility.Outputs.Length > 0) resController.ResourceControl.UpdateOutputs(_facility.Outputs.ToList());
            if(_facility.Inputs.Length > 0) resController.ResourceControl.UpdateInputs(_facility.Inputs.ToList());
            if(_facility.RequiredRawResources.Length > 0) resController.ResourceControl.UpdateUsedRawResources(_facility.RequiredRawResources.ToList());
        }
        ResourceManager resManager = GameObject.FindObjectOfType<ResourceManager>();
        if(resManager != null)
        {
            List<Resource> costs = new List<Resource>();
            foreach(Resource res in _facility.ResourceCost)
            {
                Resource resCost = res;
                resCost.Amount = -resCost.Amount;
                costs.Add(resCost);
            }
            resManager.UpdateTotalResources(costs);
        }
        BuildSlots.Add(_facility);
    }
}
