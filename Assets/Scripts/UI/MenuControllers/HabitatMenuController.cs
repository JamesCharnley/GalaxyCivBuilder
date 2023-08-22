using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatMenuController : MenuController
{
    [SerializeField] protected HabitatStatsPanel HabitatStatsGrid;
    [SerializeField] protected GameObject HabitatStatPrefab;
    public override void InitialiseMenu(InteractableObject interactableObject)
    {
        base.InitialiseMenu(interactableObject);

        IHabitat habitat = interactableObject as IHabitat;
        if(habitat != null)
        {
            GameObject go = Instantiate(HabitatStatPrefab);
            TMPro.TMP_Text text = go.GetComponent<TMPro.TMP_Text>();
            text.text = "Population: " + habitat.HabitatControl.Population.ToString();
            go.transform.SetParent(HabitatStatsGrid.transform);
        }
    }
}
