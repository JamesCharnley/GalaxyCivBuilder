using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClick();
        }
    }

    void LeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            IInteractableRender interactable = hit.transform.GetComponent<IInteractableRender>();
            if(interactable != null ) 
            {
                UIManager uiManager = FindObjectOfType<UIManager>();
                if (uiManager)
                {
                    uiManager.OpenObjectMenu(interactable.Interactable);
                }
            }
        }
    }
}
