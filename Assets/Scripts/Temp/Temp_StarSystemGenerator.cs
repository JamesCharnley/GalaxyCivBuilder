using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_StarSystemGenerator : MonoBehaviour
{
    [SerializeField] GameObject StarSystemRenderPrefab;
    [SerializeField] InteractableTemplate[] interactableTemplates;
    StarSystem CreatedStarSystem = null;

    public void Start()
    {
        CreatedStarSystem = new StarSystem();
        GenerateStarSystem();
    }

    void GenerateStarSystem()
    {
        foreach(InteractableTemplate interactableTemplate in interactableTemplates)
        {
            switch (interactableTemplate.Type)
            {
                case EInteractableType.Planet:
                    GeneratePlanet(interactableTemplate);
                    break;
                default:
                    break;
            }
        }

        SpawnStarSystem();
    }

    void GeneratePlanet(InteractableTemplate _template)
    {
        Temp_PlanetGenerator pg = FindObjectOfType<Temp_PlanetGenerator>();
        if(pg != null)
        {
            Planet newPlanet = pg.GeneratePlanet(_template);
            CreatedStarSystem.Contents.Add(newPlanet);
        }
    }

    void SpawnStarSystem()
    {
        GameObject go = Instantiate(StarSystemRenderPrefab);
        go.transform.position = new Vector3(-40, 0, 0);
        StarSystemRender sr = go.GetComponent<StarSystemRender>();
        if(sr != null)
        {
            sr.SetData(CreatedStarSystem);
        }
    }
}
