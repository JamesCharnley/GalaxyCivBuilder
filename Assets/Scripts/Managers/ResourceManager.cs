using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EResource
{
    None,
    Energy,
    Food,
    Metal,
    RareMetal,
    Carbon,
    Silica,
    Water,
    Uranium
}

[System.Serializable]
public struct FacilityData
{
    public EFacility Facility;
    public string Name;
    public string Description;
    public Sprite DisplayImage;
    public Resource[] ResourceCost;
    public ResourceInOut[] InputOutput;
}

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceStrings ResourceNames;
    [SerializeField] private FacilityDataDB FacilityDB; 

    public Dictionary<EResource, string> ResourceNameDB = new Dictionary<EResource, string>();
    public Dictionary<EFacility, FacilityData> FacilityInfoDatabase = new Dictionary<EFacility, FacilityData>();
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach(EResource resource in ResourceNames.ResourceEnums)
        {
            if(ResourceNames.ResourceNames.Length > index)
            {
                ResourceNameDB.Add(resource, ResourceNames.ResourceNames[index]);
            }
            index++;
        }
        foreach(FacilityData data in FacilityDB.Facilities)
        {
            FacilityInfoDatabase.Add(data.Facility, data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
