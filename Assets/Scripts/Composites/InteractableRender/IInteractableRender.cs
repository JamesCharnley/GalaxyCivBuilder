using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableRender
{
    public InteractableObject Interactable { get; set; }

    public void InteractAction();
}
