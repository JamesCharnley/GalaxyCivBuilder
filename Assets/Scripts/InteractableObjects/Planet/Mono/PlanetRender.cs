using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRender : MonoBehaviour, IInteractableRender
{

    public Planet PlanetData { get; set; }
    public InteractableObject Interactable { get; set; }

    public void InteractAction()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager)
        {
            uiManager.OpenObjectMenu(Interactable);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Interactable = PlanetData;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
