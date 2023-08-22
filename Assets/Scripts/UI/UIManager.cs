using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMenuType
{
    Planet,
    Ship
}
public class UIManager : MonoBehaviour
{
    [SerializeField] MenuController PlanetMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenObjectMenu(InteractableObject interactableObject)
    {
        switch(interactableObject.MenuType)
        {
            case EMenuType.Planet:
                PlanetMenu.InitialiseMenu(interactableObject);
                break;
        }
    }
}
