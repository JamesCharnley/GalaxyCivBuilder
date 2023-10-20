using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableTemplate", menuName = "ScriptableObjects/InteractableTemplate")]
public class InteractableTemplate : ScriptableObject
{
    public FRenderInfo RenderInfo;
    public EInteractableType Type;
}
