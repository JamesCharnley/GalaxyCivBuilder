using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceStrings ResourceNames;

    public Dictionary<EResource, string> ResourceNameDB = new Dictionary<EResource, string>();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
