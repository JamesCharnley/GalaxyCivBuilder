using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EResource
{
    None,
    Energy,
    Food
}
[System.Serializable]
public struct ResourceInOut
{
    public EResource Resource;
    public int CurrentAmount;
    public int MaxAmount;
}

public class ResourceController : MonoBehaviour
{
    public IResourceController OwnerInterface { get; set; }

    public void RegisterController()
    {

    }
    public void DeRegisterController()
    {

    }
    
}
