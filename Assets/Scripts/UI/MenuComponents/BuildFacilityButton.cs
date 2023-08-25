using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildFacilityButton : MonoBehaviour
{
    [HideInInspector] public FacilityData Data;
    [HideInInspector] public IBuildable BuildableInterface;
    [HideInInspector] public BuildableMenuBuilder MenuBuilder;
    [SerializeField] private Image DisplayImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(FacilityData _data)
    {
        Data = _data;
        DisplayImage.sprite = Data.DisplayImage;
    }

    public void OnClick()
    {
        if(CanBuild())
        {
            BuildFacility();
        }

    }

    bool CanBuild()
    { 
        if(BuildableInterface != null)
        {
            if(BuildableInterface.BuildableControl.CanBuild(Data))
            {
                return true;
            }
            return false;
        }
        Debug.Log("Cannot build Facility: BuildableInterface is null");
        return false; 
    }

    private void BuildFacility()
    {
        if(BuildableInterface != null)
        {
            BuildableInterface.BuildableControl.BuildFacility(Data);
        }
        if(MenuBuilder != null)
        {
            MenuBuilder.BuildFacility(Data);
        }
    }
}
