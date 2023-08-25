using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableMenuBuilder : MonoBehaviour
{
    [SerializeField] GameObject BuildablePanel;
    [SerializeField] GameObject BuildableCraftPanel;
    [SerializeField] GameObject FacilitySlotPrefab;
    [SerializeField] GameObject FacilityBuildButtonPrefab;
    List<BuildSlot> BuildSlots = new List<BuildSlot>();
    InteractableObject SelectedObject;
    public void InitialiseMenu(InteractableObject interactableObject)
    {
        SelectedObject = interactableObject;
        IBuildable buildable = interactableObject as IBuildable;
        if (buildable != null)
        {
            for (int i = 0; i < buildable.BuildableControl.CurrentSlots; i++)
            {
                GameObject go = Instantiate(FacilitySlotPrefab);
                go.transform.SetParent(BuildablePanel.transform, false);
                BuildSlot buildSlot = go.GetComponent<BuildSlot>();
                buildSlot.EmptySlot();
                BuildSlots.Add(buildSlot);
                Debug.Log("Instantiated BuildSlot");
            }

            int index = 0;
            foreach (FacilityData data in buildable.BuildableControl.BuildSlots)
            {
                BuildSlots[index].SetData(data);
                index++;
            }
            ResourceManager resManager = FindObjectOfType<ResourceManager>();
            if(resManager != null)
            {
                foreach (EFacility facName in buildable.BuildableControl.CompatibleFacilities)
                {
                    FacilityData facData = resManager.FacilityInfoDatabase[facName];
                    GameObject go = Instantiate(FacilityBuildButtonPrefab);
                    go.transform.SetParent(BuildableCraftPanel.transform, false);
                    BuildFacilityButton buildFacilityButton = go.GetComponent<BuildFacilityButton>();
                    buildFacilityButton.SetData(facData);
                    buildFacilityButton.BuildableInterface = interactableObject as IBuildable;
                    buildFacilityButton.MenuBuilder = this;
                }
            }
            
        }
    }
    public void CLearMenu()
    {
        for (int i = BuildablePanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(BuildablePanel.transform.GetChild(i).gameObject);
            BuildSlots.Clear();
        }
        for (int i = BuildableCraftPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(BuildableCraftPanel.transform.GetChild(i).gameObject);
        }
    }

    public void BuildFacility(FacilityData _data)
    {
        foreach(BuildSlot slot in BuildSlots)
        {
            if(!slot.IsOccupied())
            {
                slot.SetData(_data);
                break;
            }
        }
    }
}
