using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct FCameraTransform
{
    public Vector3 Position;
    public float Size;
}

public class StarSystemSpawner : MonoBehaviour
{
    StarSystem currentStarSystem = null;

    [SerializeField] Button ExitStarSyatemButton;
    [SerializeField] Camera Cam;
    FCameraTransform CamData;
    bool InStarSystem = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateStarSystemView(StarSystem _starSystem)
    {
        if(!InStarSystem)
        {
            CamData.Position = Cam.transform.position;
            CamData.Size = Cam.orthographicSize;
        }

        if(currentStarSystem != null)
        {
            ClearStarSystemContainer();
        }

        // activate exit starsystem button
        ExitStarSyatemButton.enabled = true;

        currentStarSystem = _starSystem;

        SpawnStarSystem();

        // move camera to starsystem view
        Cam.transform.position = new Vector3(0,0,-10);
        Cam.orthographicSize = 70;

        InStarSystem = true;
    }

    public void ActivateGalaxyView()
    {
        // move camera to saved data
        Cam.transform.position = CamData.Position;
        Cam.orthographicSize = CamData.Size;

        InStarSystem = false;
    }
    void SpawnStarSystem()
    {
        foreach(InteractableObject obj in currentStarSystem.Contents)
        {
            Planet planet = obj as Planet;
            if(planet != null)
            {
                BuildPlanet(planet);
                continue;
            }
        }
    }

    void ClearStarSystemContainer()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        currentStarSystem = null;
    }

    void BuildPlanet(Planet _planet)
    {
        GameObject parent = new GameObject("planet");
        parent.AddComponent<PlanetRender>();
        parent.AddComponent<CircleCollider2D>();

        PlanetRender pr = parent.GetComponent<PlanetRender>();
        pr.PlanetData = _planet;

        CircleCollider2D circleCollider = parent.GetComponent<CircleCollider2D>();
        circleCollider.radius = 3.5f;

        parent.transform.SetParent(transform, false);
        parent.transform.localPosition = _planet.RenderInfo.Position;

        foreach(FSpriteInfo info in _planet.RenderInfo.Images)
        {
            GameObject childImage = new GameObject();
            childImage.AddComponent<SpriteRenderer>();
            SpriteRenderer sr = childImage.GetComponent<SpriteRenderer>();
            sr.sprite = info.Image;
            sr.sortingOrder = info.ImageLayer;
            childImage.transform.SetParent(parent.transform, false);
            childImage.transform.localPosition = info.ImageLocalPosition;
            childImage.transform.localScale = info.ImageScale;
        }
        

    }

    public void ExitStarSystem()
    {
        ExitStarSyatemButton.enabled = false;

        ClearStarSystemContainer();

        ActivateGalaxyView();
    }
    
}
