using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AR_Template", menuName = "ScriptableObjects/AvailableResources")]
public class AvailableResourcesTemplate : ScriptableObject
{
    public List<Resource> AvailableResources;
    public List<Resource> AvailableBaseResources;
}
