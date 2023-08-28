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
            List<Resource> inOuts = ResController.ResourceControl.GetFinalInOutValues();
            foreach(Resource res in inOuts)
            {
                GameObject statPref = Instantiate(StatsTextPrefab);
                statPref.transform.SetParent(ResourceStatsPanel.transform, false);
                TMPro.TMP_Text text = statPref.GetComponent<TMPro.TMP_Text>();
                if (res.Amount > 0)
                {
                    text.text = res.Name + ": +" + res.Amount.ToString();
                }
                else
                {
                    text.text = res.Name + ": " + res.Amount.ToString();
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
