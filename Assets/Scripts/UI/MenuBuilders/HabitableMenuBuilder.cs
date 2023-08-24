using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitableMenuBuilder : MonoBehaviour
{
    [SerializeField] protected HabitatStatsPanel HabitatStatsGrid;
    [SerializeField] protected GameObject HabitatStatPrefab;
    public void InitialiseMenu(InteractableObject interactableObject)
    {

        IHabitat habitat = interactableObject as IHabitat;
        if (habitat != null)
        {
            GameObject go = Instantiate(HabitatStatPrefab);
            TMPro.TMP_Text text = go.GetComponent<TMPro.TMP_Text>();
            text.text = "Population: " + habitat.HabitatControl.Population.ToString();
            go.transform.SetParent(HabitatStatsGrid.transform);
        }
    }
    public void CLearMenu()
    {
        for (int i = HabitatStatsGrid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(HabitatStatsGrid.transform.GetChild(i).gameObject);
        }
    }
}
