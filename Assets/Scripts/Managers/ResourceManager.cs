using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EResource
{
    None,
    Quartz,
    Carbon,
    Iron,
    Titanium,
    Uranium,
    Water,
    Lithium,
    FertileLand,
    Solar,
    Wind,
    Thermal,
    Food,
    Energy,
    PureSilicon,
    Steel,
    EnrichedUranium,
    Deuterium,
    Tritium,
    Polymer,
    CarbonFibre,
    DeuteriumTritium
}

[System.Serializable]
public struct FacilityData
{
    public EFacility Facility;
    public string Name;
    public string Description;
    public Sprite DisplayImage;
    public Resource[] ResourceCost;
    public Resource[] Inputs;
    public Resource[] Outputs;
    public Resource[] RequiredBaseResources;
}

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceDatabase ResourceDB;
    [SerializeField] private FacilityDataDB FacilityDB; 

    public Dictionary<EResource, Resource> ResourceDataDB = new Dictionary<EResource, Resource>();
    public Dictionary<EFacility, FacilityData> FacilityInfoDatabase = new Dictionary<EFacility, FacilityData>();

    public Dictionary<EResource, int> TotalResources = new Dictionary<EResource, int>();

    List<IResourceController> ResourceControllers = new List<IResourceController>();
    // Start is called before the first frame update
    void Start()
    {
        // fill dictionary with ResourceDatabase
        foreach(Resource res in ResourceDB.Resources)
        {
            ResourceDataDB.Add(res.ResourceName, res);
        }
        // Add all resources to totals with a value of zero
        foreach(Resource resource in ResourceDB.Resources)
        {
            TotalResources.Add(resource.ResourceName, 0);

        }

        // Add resource names to all inputs and outputs and required resources of facilities
        foreach(FacilityData data in FacilityDB.Facilities)
        {
            int count = 0;
            foreach(Resource inRes in data.Inputs)
            {
                foreach(Resource resDB in ResourceDB.Resources)
                {
                    if(resDB.ResourceName == inRes.ResourceName)
                    {
                        data.Inputs[count].Name = resDB.Name;
                        break;
                    }
                }
                count++;
            }
            count = 0;
            foreach (Resource outRes in data.Outputs)
            {
                foreach (Resource resDB in ResourceDB.Resources)
                {
                    if (resDB.ResourceName == outRes.ResourceName)
                    {
                        data.Outputs[count].Name = resDB.Name;
                        break;
                    }
                }
                count++;
            }
            count = 0;
            foreach (Resource reqRes in data.RequiredBaseResources)
            {
                foreach (Resource resDB in ResourceDB.Resources)
                {
                    if (resDB.ResourceName == reqRes.ResourceName)
                    {
                        data.RequiredBaseResources[count].Name = resDB.Name;
                        break;
                    }
                }
                count++;
            }
            FacilityInfoDatabase.Add(data.Facility, data);
        }

        StartCoroutine(UpdateResourceControllers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterResourceController(IResourceController _resourceController)
    {
        ResourceControllers.Add(_resourceController);
    }

    IEnumerator UpdateResourceControllers()
    {
        yield return new WaitForSeconds(1.0f);

        foreach(IResourceController controller in ResourceControllers)
        {
            controller.ResourceControl.UpdateResourceManager(this);
        }

        StartCoroutine(UpdateResourceControllers());
    }

    public void UpdateTotalResources(List<Resource> _changes)
    {
        foreach(Resource res in _changes)
        {
            TotalResources[res.ResourceName] += res.Amount;
        }
    }
}
