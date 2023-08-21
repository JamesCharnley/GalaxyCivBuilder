using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_PlanetGenerator : MonoBehaviour
{
    [SerializeField] BuildableTemplate[] buildableTemplates;
    [SerializeField] AvailableResourcesTemplate[] availableResourcesTemplates;

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

        Planet newPlanet = new Planet(resourcesTemplate, buildableTemplate);

        GalaxyManager.Planets.Add(newPlanet);

        GameObject planetRender = Instantiate(planetRenderPrefab);
        planetRender.GetComponent<PlanetRender>().PlanetData = newPlanet;
        planetRender.GetComponent<PlanetRender>().UpdateVariables();

    }
}
