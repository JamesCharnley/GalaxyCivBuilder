using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableResourceMenuBuilder : MonoBehaviour
{
    [SerializeField] GameObject AvailableResourcesPanel;
    [SerializeField] private GameObject StatsTextPrefab;
    public void InitialiseMenu(InteractableObject interactableObject)
    {
        IAvailableResources availableResources = interactableObject as IAvailableResources;
        IResourceController resourceController = interactableObject as IResourceController;
        if(availableResources != null && resourceController != null)
        {
            foreach(RawResource rawResource in availableResources.AvailableResourcesControl.RawResources) 
            {
                string totalRawResourceText = rawResource.Amount.ToString();
                string usedRawResourceText = "0";
                foreach(RawResource usedRawResource in resourceController.ResourceControl.UsedRawResources)
                {
                    if(usedRawResource.ResourceName == rawResource.ResourceName)
                    {
                        usedRawResourceText = usedRawResource.Amount.ToString();
                        break;
                    }
                }

                GameObject go = Instantiate(StatsTextPrefab);
                TMPro.TMP_Text text = go.GetComponent<TMPro.TMP_Text>();
                text.text = rawResource.ResourceName.ToString() + ": " + usedRawResourceText + "/" + totalRawResourceText;

                go.transform.SetParent(AvailableResourcesPanel.transform, false);
            }
            foreach(Resource res in availableResources.AvailableResourcesControl.Resources)
            {
                string totalResourceText = res.Amount.ToString();
                string usedResourceText = "0";
                foreach(ResourceInOut usedRes in resourceController.ResourceControl.Outputs)
                {
                    if(res.ResourceName == usedRes.Resource)
                    {
                        usedResourceText = usedRes.CurrentAmount.ToString();
                        break;
                    }
                }

                GameObject go = Instantiate(StatsTextPrefab);
                TMPro.TMP_Text text = go.GetComponent<TMPro.TMP_Text>();
                text.text = res.ResourceName.ToString() + ": " + usedResourceText + "/" + totalResourceText;

                go.transform.SetParent(AvailableResourcesPanel.transform, false);
            }
        }
    }

    public void CLearMenu()
    {
        for (int i = AvailableResourcesPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(AvailableResourcesPanel.transform.GetChild(i).gameObject);
        }
    }
}
