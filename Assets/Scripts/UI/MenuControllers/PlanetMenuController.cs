using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMenuController : HabitatMenuController
{
    [SerializeField] GameObject BuildablePanel;
    [SerializeField] GameObject FacilitySlotPrefab;
    List<BuildSlot> BuildSlots = new List<BuildSlot>();
    public override void InitialiseMenu(InteractableObject interactableObject)
    {
        base.InitialiseMenu(interactableObject);

        IBuildable buildable = interactableObject as IBuildable;
        if(buildable != null)
        {
            for(int i = 0; i < buildable.BuildableControl.CurrentSlots; i++)
            {
                GameObject go = Instantiate(FacilitySlotPrefab);
                go.transform.SetParent(BuildablePanel.transform, false);
                BuildSlot buildSlot = go.GetComponent<BuildSlot>();
                buildSlot.EmptySlot();
                BuildSlots.Add(buildSlot);
                Debug.Log("Instantiated BuildSlot");
            }

            int index = 0;
            foreach(FacilityData data in buildable.BuildableControl.BuildSlots)
            {
                BuildSlots[index].SetData(data);
                index++;
            }
        }
    }
}
