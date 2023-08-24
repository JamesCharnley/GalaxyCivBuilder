using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public int Amount;
}
[System.Serializable]
public struct RawResource
{
    public ERawResource ResourceName;
    public int Amount;
}
public class ResourceController
{
    public List<ResourceInOut> Inputs { get; set; }
    public List<ResourceInOut> Outputs { get; set; }
    public List<RawResource> UsedRawResources { get; set; }

    IResourceController OwnerInterface { get; set; }

    public ResourceController(List<ResourceInOut> inputs, List<ResourceInOut> outputs, List<RawResource> usedRawResources, IResourceController ownerInterface)
    {
        Inputs = inputs;
        Outputs = outputs;
        UsedRawResources = usedRawResources;
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
            bool inputExists = false;
            for(int i = 0; i < Inputs.Count; i++)
            {
                if(res.ResourceName == Inputs[i].Resource)
                {
                    ResourceInOut resCopy = Inputs[i];
                    resCopy.CurrentAmount += res.Amount;
                    Inputs[i] = resCopy;
                    inputExists = true;
                    break;
                }
            }
            if(inputExists == false)
            {
                ResourceInOut newInput = new ResourceInOut();
                newInput.Resource = res.ResourceName;
                newInput.CurrentAmount = res.Amount;
                Inputs.Add(newInput);
            }
            
        }
    }
    public void UpdateOutputs(List<Resource> _outputs)
    {
        foreach (Resource res in _outputs)
        {
            bool outputExists = false;
            for (int i = 0; i < Outputs.Count; i++)
            {
                if (res.ResourceName == Outputs[i].Resource)
                {
                    ResourceInOut resCopy = Outputs[i];
                    resCopy.CurrentAmount += res.Amount;
                    Outputs[i] = resCopy;
                    outputExists = true;
                    break;
                }
            }
            if(outputExists == false)
            {
                ResourceInOut newOutput = new ResourceInOut();
                newOutput.Resource = res.ResourceName;
                newOutput.CurrentAmount = res.Amount;
                Outputs.Add(newOutput);
            }
        }
    }
    public void UpdateUsedRawResources(List<RawResource> _usedResources)
    {
        foreach (RawResource res in _usedResources)
        {
            bool resExists = false;
            for (int i = 0; i < UsedRawResources.Count; i++)
            {
                if (res.ResourceName == UsedRawResources[i].ResourceName)
                {
                    RawResource resCopy = UsedRawResources[i];
                    resCopy.Amount += res.Amount;
                    UsedRawResources[i] = resCopy;
                    resExists = true;
                    break;
                }
            }
            if (resExists == false)
            {
                RawResource newRes = new RawResource();
                newRes.ResourceName = res.ResourceName;
                newRes.Amount = res.Amount;
                UsedRawResources.Add(newRes);
            }
        }
    }

    public void UpdateResourceManager(ResourceManager _manager)
    {
        List<Resource> resourceChanges = new List<Resource>();
        List<EResource> resources = new List<EResource>();
        foreach(ResourceInOut input in Inputs)
        {
            resources.Add(input.Resource);
        }
        foreach (ResourceInOut output in Outputs)
        {
            bool exists = false;
            foreach (ResourceInOut input in Inputs)
            {
                if(output.Resource == input.Resource)
                {
                    exists = true;
                }
            }
            if(!exists)
            {
                resources.Add(output.Resource);
            }
        }

        foreach (EResource resource in resources)
        {
            int total = 0;
            foreach (ResourceInOut output in Outputs)
            {
                if (resource == output.Resource)
                {
                    total += output.CurrentAmount;
                    break;
                }
            }
            foreach (ResourceInOut input in Inputs)
            {
                if (resource == input.Resource)
                {
                    total -= input.CurrentAmount;
                }
            }

            if(total != 0)
            {
                Resource res = new Resource();
                res.ResourceName = resource;
                res.Amount = total;
                resourceChanges.Add(res);
            }
        }

        _manager.UpdateTotalResources(resourceChanges);
    }
}
