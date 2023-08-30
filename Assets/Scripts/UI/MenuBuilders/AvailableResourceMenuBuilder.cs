using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableResourceMenuBuilder : MonoBehaviour
{
    [SerializeField] GameObject AvailableResourcesPanel;
    [SerializeField] private GameObject StatsTextPrefab;
    public void InitialiseMenu(InteractableObject interactableObject)
    {
        IResourceController resourceController = interactableObject as IResourceController;
        if(resourceController != null)
        {
            foreach(KeyValuePair<EResource, Resource> baseKVP in resourceController.ResourceControl.BaseResources) 
            {
                string totalBaseResourceText = baseKVP.Value.Amount.ToString();
                string totalOccupiedBaseResourceText = "0";
                if(resourceController.ResourceControl.OccupiedResources.ContainsKey(baseKVP.Key))
                {
                    totalOccupiedBaseResourceText = resourceController.ResourceControl.OccupiedResources[baseKVP.Key].Amount.ToString();
                }

                GameObject go = Instantiate(StatsTextPrefab);
                TMPro.TMP_Text text = go.GetComponent<TMPro.TMP_Text>();
                text.text = baseKVP.Value.Name + ": " + totalOccupiedBaseResourceText + "/" + totalBaseResourceText;

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
