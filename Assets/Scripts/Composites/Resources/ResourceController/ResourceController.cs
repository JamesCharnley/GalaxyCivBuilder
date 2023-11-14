using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


[System.Serializable]
public struct ResourceInOut
{
    public EResource Resource;
    public int CurrentAmount;
    public int MaxAmount;
}
[System.Serializable]
public struct Resource
{
    public EResource ResourceName;
    public string Name;
    public int Amount;
}


public class ResourceController
{
    public Dictionary<EResource, Resource> BaseResources = new Dictionary<EResource, Resource>();
    public Dictionary<EResource, Resource> OccupiedResources = new Dictionary<EResource, Resource>();
    public Dictionary<EResource, Resource> Inputs = new Dictionary<EResource, Resource>();
    public Dictionary<EResource, Resource> Outputs = new Dictionary<EResource, Resource>();
    public Dictionary<EResource, Resource> ExcessOutputs = new Dictionary<EResource, Resource>();
    public int ExcessOutputCapacity = 100;
    int totalExcessStored = 0;
    //public List<ResourceInOut> Inputs { get; set; }
    //public List<ResourceInOut> Outputs { get; set; }
    //public List<RawResource> UsedRawResources { get; set; }

    IResourceController OwnerInterface { get; set; }

    public ResourceController(List<Resource> inputs, List<Resource> outputs, List<Resource> occupiedResources, List<Resource> baseResources, IResourceController ownerInterface)
    {
        //Inputs = inputs;
        //Outputs = outputs;
        //UsedRawResources = usedRawResources;

        ResourceManager resManager = GameObject.FindObjectOfType<ResourceManager>();

        foreach (Resource input in inputs)
        {
            Inputs.Add(input.ResourceName, input);
        }
        foreach (Resource output in outputs)
        {
            Outputs.Add(output.ResourceName, output);
        }
        foreach(Resource baseResource in baseResources)
        {
            Resource resDB = resManager.ResourceDataDB[baseResource.ResourceName];
            Resource entry = new Resource();
            entry.ResourceName = baseResource.ResourceName;
            entry.Name = resDB.Name;
            entry.Amount = baseResource.Amount;
            BaseResources.Add(baseResource.ResourceName, entry);
        }
        foreach(Resource occupiedResource in occupiedResources)
        {
            OccupiedResources.Add(occupiedResource.ResourceName, occupiedResource);
        }

        OwnerInterface = ownerInterface;

        RegisterController();
    }

    public void RegisterController()
    {
        ResourceManager rm = GameObject.FindObjectOfType<ResourceManager>();
        if(rm != null )
        {
            rm.RegisterResourceController(OwnerInterface);
        }
    }
    public void DeRegisterController()
    {

    }

    public void UpdateInputs(List<Resource> _inputs)
    {
        foreach(Resource res in _inputs)
        {
            if(Inputs.ContainsKey(res.ResourceName))
            {
                Resource resCopy = Inputs[res.ResourceName];
                resCopy.Amount += res.Amount;
                Inputs[res.ResourceName] = resCopy;
            }
            else
            {
                Inputs.Add(res.ResourceName, res);
            }
        }
    }
    public void UpdateOutputs(List<Resource> _outputs)
    {
        foreach (Resource res in _outputs)
        {
            if (Outputs.ContainsKey(res.ResourceName))
            {
                Resource resCopy = Outputs[res.ResourceName];
                resCopy.Amount += res.Amount;
                Outputs[res.ResourceName] = resCopy;
            }
            else
            {
                Outputs.Add(res.ResourceName, res);
            }
        }
    }
    public void UpdateOccupiedResources(List<Resource> _usedResources)
    {
        foreach (Resource res in _usedResources)
        {
            if (OccupiedResources.ContainsKey(res.ResourceName))
            {
                Resource resCopy = OccupiedResources[res.ResourceName];
                resCopy.Amount += res.Amount;
                OccupiedResources[res.ResourceName] = resCopy;
            }
            else
            {
                OccupiedResources.Add(res.ResourceName, res);
            }
        }
    }

    public void UpdateExcessResources(List<Resource> _excessResources)
    {

        if(totalExcessStored < ExcessOutputCapacity)
        {
            foreach (Resource resource in _excessResources)
            {
                if (resource.Amount > 0)
                {
                    if (ExcessOutputs.ContainsKey(resource.ResourceName))
                    {
                        int adjustedValue = resource.Amount + ExcessOutputs[resource.ResourceName].Amount;
                        if(adjustedValue != 0)
                        {
                            Resource resCopy = ExcessOutputs[resource.ResourceName];
                            resCopy.Amount = adjustedValue;
                            ExcessOutputs[resource.ResourceName] = resCopy;
                        }
                        else
                        {
                            ExcessOutputs.Remove(resource.ResourceName);
                        }
                    }
                    else
                    {
                        ExcessOutputs.Add(resource.ResourceName, resource);
                    }
                }
            }
        }

        foreach (Resource resource in _excessResources)
        {
            if (resource.Amount < 0)
            {
                if (ExcessOutputs.ContainsKey(resource.ResourceName))
                {
                    int adjustedValue = resource.Amount + ExcessOutputs[resource.ResourceName].Amount;
                    if(adjustedValue != 0)
                    {
                        Resource resCopy = ExcessOutputs[resource.ResourceName];
                        resCopy.Amount = adjustedValue;
                        ExcessOutputs[resource.ResourceName] = resCopy;
                    }
                    else
                    {
                        ExcessOutputs.Remove(resource.ResourceName);
                    }
                }
                else
                {
                    ExcessOutputs.Add(resource.ResourceName, resource);
                }
            }
        }

        totalExcessStored = 0;
        foreach (KeyValuePair<EResource, Resource> kvp in ExcessOutputs)
        {
            if(kvp.Value.Amount > 0)
            {
                totalExcessStored += kvp.Value.Amount;
            }
        }
    }

    public void UpdateResourceManager(ResourceManager _manager)
    {
        List<Resource> finalInOuts = GetFinalInOutValues();
        _manager.UpdateTotalResources(finalInOuts);
        UpdateExcessResources(finalInOuts);
    }

    public List<Resource> GetFinalInOutValues()
    {
        Dictionary<EResource, Resource> resources = new Dictionary<EResource, Resource>();
        resources = Outputs;

        foreach (KeyValuePair<EResource, Resource> kvp in Inputs)
        {
            if (resources.ContainsKey(kvp.Key))
            {
                Resource resCopy = resources[kvp.Key];
                resCopy.Amount -= kvp.Value.Amount;
                resources[kvp.Key] = resCopy;
            }
            else
            {
                Resource resCopy = kvp.Value;
                resCopy.Amount = -kvp.Value.Amount;
                resources.Add(kvp.Key, resCopy);
            }
        }

        List<Resource> resChanges = new List<Resource>();
        foreach (KeyValuePair<EResource, Resource> kvp in resources)
        {
            resChanges.Add(kvp.Value);
        }

        return resChanges;
    }

    public List<Resource> GetExcessResources()
    {
        List<Resource> excessResources = new();

        foreach(KeyValuePair<EResource, Resource> kvp in ExcessOutputs)
        {
            excessResources.Add(kvp.Value);
        }

        return excessResources;
    }
}
