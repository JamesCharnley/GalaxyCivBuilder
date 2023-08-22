using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private EResource[] _resourceEnums;
    [SerializeField] private string[] _resourceNames;

    public Dictionary<EResource, string> ResourceNameDB = new Dictionary<EResource, string>();
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach(EResource resource in _resourceEnums)
        {
            if(_resourceNames.Length > index)
            {
                ResourceNameDB.Add(resource, _resourceNames[index]);
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
