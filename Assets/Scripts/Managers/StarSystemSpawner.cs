using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemSpawner : MonoBehaviour
{
    StarSystem currentStarSystem = null;
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
        if(currentStarSystem != null)
        {
            ClearStarSystemContainer();
        }

        currentStarSystem = _starSystem;

        SpawnStarSystem();
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
    
}
