using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RC_Template", menuName = "ScriptableObjects/ResourceController")]
public class ResourceControllerTemplate : ScriptableObject
{
    public List<Resource> Inputs;
    public List<Resource> Outputs;
    public List<Resource> BaseResources;
}
