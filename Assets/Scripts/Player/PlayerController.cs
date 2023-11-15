using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float CameraMoveSpeed = 5;
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

        float horAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");
        if(horAxis != 0 || vertAxis != 0)
        {
            MoveCamera(horAxis, vertAxis);
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
                interactable.InteractAction();
            }
        }
    }
    void MoveCamera(float _x, float _y)
    {
        Vector3 dir = new Vector3(_x, _y, 0);
        dir.Normalize();

        Camera.main.transform.position += dir * CameraMoveSpeed * Time.deltaTime;
    }
}
