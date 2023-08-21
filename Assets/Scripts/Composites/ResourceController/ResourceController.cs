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
[System.Serializable]
public struct Resource
{
    public EResource ResourceName;
    public int Amount;
}
public class ResourceController
{
    public List<ResourceInOut> Inputs { get; set; }
    public List<ResourceInOut> Outputs { get; set; }

    public void RegisterController()
    {

    }
    public void DeRegisterController()
    {

    }
    
}
