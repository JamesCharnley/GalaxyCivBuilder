using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMenuBuilder : MonoBehaviour
{
    [SerializeField] private GameObject ResourceStatsPanel;
    [SerializeField] private GameObject StatsTextPrefab;
    public void InitialiseMenu(InteractableObject interactableObject)
    {
        IResourceController ResController = interactableObject as IResourceController;
        if(ResController != null)
        {
            List<Resource> resourceTotals = new List<Resource>();
            foreach(ResourceInOut inp in ResController.ResourceControl.Inputs)
            {
                Debug.Log("Input: " + inp.Resource);
                Resource res = new Resource();
                res.ResourceName = inp.Resource;
                res.Amount = -inp.CurrentAmount;
                resourceTotals.Add(res);
            }
            
            foreach(ResourceInOut outp in ResController.ResourceControl.Outputs)
            {
                Debug.Log("Output: " + outp.Resource);
                int index = 0;
                bool exists = false;
                foreach(Resource existingRes in resourceTotals)
                {
                    if(existingRes.ResourceName == outp.Resource)
                    {
                        Resource resCopy = existingRes;
                        resCopy.Amount += outp.CurrentAmount;
                        resourceTotals[index] = resCopy;
                        exists = true;
                        break;
                    }
                    index++;
                }
                if(!exists)
                {
                    Resource res = new Resource();
                    res.ResourceName = outp.Resource;
                    res.Amount = outp.CurrentAmount;
                    resourceTotals.Add(res);
                }
            }

            foreach(Resource res in resourceTotals)
            {
                Debug.Log(res.ResourceName + " = " + res.Amount);
            }
        }
    }
}
