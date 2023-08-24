using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableMenuBuilder : MonoBehaviour
{
    [SerializeField] GameObject BuildablePanel;
    [SerializeField] GameObject FacilitySlotPrefab;
    List<BuildSlot> BuildSlots = new List<BuildSlot>();
    public void InitialiseMenu(InteractableObject interactableObject)
    {

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
        }
    }
    public void CLearMenu()
    {
        for (int i = BuildablePanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(BuildablePanel.transform.GetChild(i).gameObject);
            BuildSlots.Clear();
        }
    }
}
