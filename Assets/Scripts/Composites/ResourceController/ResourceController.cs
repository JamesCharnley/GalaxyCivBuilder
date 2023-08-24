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

    public void RegisterController()
    {

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

}
