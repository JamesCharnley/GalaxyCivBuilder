using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceDB", menuName = "ScriptableObjects/ResourceDatabase")]
public class ResourceDatabase : ScriptableObject
{
    public Resource[] Resources;
}
