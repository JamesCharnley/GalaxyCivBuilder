using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ResourceManager ResourceManager;
    int TotalDays = 0;
    int DayCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        ResourceManager = FindObjectOfType<ResourceManager>();
        StartCoroutine(DayCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DayCycle()
    {
        if (DayCounter == 30)
        {
            MonthCycle();
            DayCounter = 0;
        }

        yield return new WaitForSeconds(1.0f);

        // update resource manager
        ResourceManager.UpdateResourceControllers();

        DayCounter++;
        TotalDays++;

        StartCoroutine(DayCycle());
    }

    void MonthCycle()
    {
        // Update import/export orders
        ResourceManager.UpdateResourceShipping();
    }
}
