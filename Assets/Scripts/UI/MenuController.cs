using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private InteractableObject SelectedObject;

    bool IsActive = false;

    [SerializeField] GameObject[] MenuParents;

    [SerializeField] TMPro.TMP_Text NameText;
    [SerializeField] TMPro.TMP_Text DescriptionText;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject menuParent in  MenuParents)
        {
            menuParent.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InitialiseMenu(InteractableObject interactableObject)
    {
        if (IsActive)
        {
            // clear data
        }
        else
        {
            EnableChildren();
        }
        SelectedObject = interactableObject;
        NameText.text = interactableObject.DisplayInformation.Name;
        DescriptionText.text = interactableObject.DisplayInformation.Description;
    }
    void EnableChildren()
    {
        foreach (GameObject menuParent in MenuParents)
        {
            menuParent.SetActive(true);
        }
    }
}
