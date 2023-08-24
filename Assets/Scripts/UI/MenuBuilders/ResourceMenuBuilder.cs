using System.Collections;
using System.Collections.Generic;
using TMPro;
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

            ResourceManager rm = FindObjectOfType<ResourceManager>();
            foreach(Resource res in resourceTotals)
            {
                if (rm)
                {
                    string resName;
                    if (rm.ResourceNameDB.TryGetValue(res.ResourceName, out resName))
                    {
                        GameObject statPref = Instantiate(StatsTextPrefab);
                        statPref.transform.SetParent(ResourceStatsPanel.transform, false);
                        TMPro.TMP_Text text = statPref.GetComponent<TMPro.TMP_Text>();
                        if(res.Amount > 0)
                        {
                            text.text = resName + ": +" + res.Amount.ToString();
                        }
                        else
                        {
                            text.text = resName + ": " + res.Amount.ToString();
                        }
                    }
                }
            
            }
        }
    }

    public void CLearMenu()
    {
        for(int i = ResourceStatsPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ResourceStatsPanel.transform.GetChild(i).gameObject);
        }
    }
}
