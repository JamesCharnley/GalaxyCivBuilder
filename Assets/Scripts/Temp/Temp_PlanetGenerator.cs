using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_PlanetGenerator : MonoBehaviour
{
    [SerializeField] BuildableTemplate[] buildableTemplates;
    [SerializeField] ResourceControllerTemplate[] resourceControllerTemplates;
    [SerializeField] DisplayInfo[] displayInfos;

    Temp_GalaxyManager GalaxyManager;

    [SerializeField] GameObject planetRenderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Planet GeneratePlanet(InteractableTemplate _interactableTemplate)
    {
        GalaxyManager = FindObjectOfType<Temp_GalaxyManager>();

        int buildableMaxRand = buildableTemplates.Length;
        int randBuildableIndex = Random.Range(0, buildableMaxRand);
        BuildableTemplate buildableTemplate = buildableTemplates[randBuildableIndex];

        int resourcesMaxRand = resourceControllerTemplates.Length;
        int randResourcesIndex = Random.Range(0, resourcesMaxRand);
        ResourceControllerTemplate resourcesTemplate = resourceControllerTemplates[randResourcesIndex];

        int dispInfoMaxRand = displayInfos.Length;
        int randDispInfoIndex = Random.Range(0, dispInfoMaxRand);
        DisplayInfo dispInfoTemplate = displayInfos[randDispInfoIndex];

        Planet newPlanet = new Planet(resourcesTemplate, buildableTemplate, dispInfoTemplate, _interactableTemplate.RenderInfo);

        return newPlanet;

    }
}
