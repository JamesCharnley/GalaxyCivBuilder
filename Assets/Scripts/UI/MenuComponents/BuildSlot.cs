using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildSlot : MonoBehaviour
{
    FacilityData Data;
    bool Occupied = false;

    [SerializeField] private Image DisplayImage;
    [SerializeField] private Sprite EmptyImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetData(FacilityData data)
    {
        Occupied = true;
        Data = data;
        if(DisplayImage == null)
        {
            Debug.Log("sdfdsfsdf");
        }
        DisplayImage.sprite = Data.DisplayImage;
    }
    public void EmptySlot()
    {
        Occupied = false;
        DisplayImage.sprite = EmptyImage;
    }

    public bool IsOccupied()
    {
        return Occupied;
    }

    public void OnClick()
    {
        if(Occupied)
        {

        }
        else
        {

        }
    }
}
