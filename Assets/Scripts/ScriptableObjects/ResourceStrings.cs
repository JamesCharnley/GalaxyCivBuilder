using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceStrings", menuName = "ScriptableObjects/ResourceStrings")]
public class ResourceStrings : ScriptableObject
{
    public EResource[] ResourceEnums;
    public string[] ResourceNames;

}
