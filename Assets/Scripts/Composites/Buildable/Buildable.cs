using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.ComponentModel.Design.Serialization;

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
        if (resController == null)
        {
            Debug.Log("Cannot build Facility: IResourceController is null");
            return false;
        }
        // Check if planet has enough required resources available
        if (_facility.RequiredBaseResources.Length > 0)
        {
            foreach (Resource reqRes in _facility.RequiredBaseResources)
            {
                int totalBaseRes = 0;
                if(resController.ResourceControl.BaseResources.ContainsKey(reqRes.ResourceName))
                {
                    totalBaseRes = resController.ResourceControl.BaseResources[reqRes.ResourceName].Amount;
                }
                else
                {
                    return false;
                }
                int totalOccupiedBaseRes = 0;
                if(resController.ResourceControl.OccupiedResources.ContainsKey(reqRes.ResourceName))
                {
                    totalOccupiedBaseRes = resController.ResourceControl.OccupiedResources[reqRes.ResourceName].Amount;
                }
                else
                {
                    return false;
                }
                int availBaseRes = totalBaseRes - totalOccupiedBaseRes;
                if(reqRes.Amount > availBaseRes)
                {
                    return false;
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
            if(_facility.RequiredBaseResources.Length > 0) resController.ResourceControl.UpdateOccupiedResources(_facility.RequiredBaseResources.ToList());
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
