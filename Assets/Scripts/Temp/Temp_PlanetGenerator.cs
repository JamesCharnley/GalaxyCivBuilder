using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_PlanetGenerator : MonoBehaviour
{
    [SerializeField] BuildableTemplate[] buildableTemplates;
    [SerializeField] AvailableResourcesTemplate[] availableResourcesTemplates;
    [SerializeField] DisplayInfo[] displayInfos;

    Temp_GalaxyManager GalaxyManager;

    [SerializeField] GameObject planetRenderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GalaxyManager = FindObjectOfType<Temp_GalaxyManager>();
        GeneratePlanet();
    }

    void GeneratePlanet()
    {
        int buildableMaxRand = buildableTemplates.Length;
        int randBuildableIndex = Random.Range(0, buildableMaxRand);
        BuildableTemplate buildableTemplate = buildableTemplates[randBuildableIndex];

        int resourcesMaxRand = availableResourcesTemplates.Length;
        int randResourcesIndex = Random.Range(0, resourcesMaxRand);
        AvailableResourcesTemplate resourcesTemplate = availableResourcesTemplates[randResourcesIndex];

        int dispInfoMaxRand = displayInfos.Length;
        int randDispInfoIndex = Random.Range(0, dispInfoMaxRand);
        DisplayInfo dispInfoTemplate = displayInfos[randDispInfoIndex];

        Planet newPlanet = new Planet(resourcesTemplate, buildableTemplate, dispInfoTemplate);

        GalaxyManager.Planets.Add(newPlanet);

        GameObject planetRender = Instantiate(planetRenderPrefab);
        planetRender.GetComponent<PlanetRender>().PlanetData = newPlanet;
        planetRender.GetComponent<PlanetRender>().UpdateVariables();

    }
}
