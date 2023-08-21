using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_PlanetGenerator : MonoBehaviour
{
    [SerializeField] BuildableTemplate[] buildableTemplates;
    [SerializeField] ResourceControllerTemplate[] resourceControllerTemplates;

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

        int resourceControllerMaxRand = resourceControllerTemplates.Length;
        int randResourceControllerIndex = Random.Range(0, resourceControllerMaxRand);
        ResourceControllerTemplate resourceControllerTemplate = resourceControllerTemplates[randResourceControllerIndex];

        Planet newPlanet = new Planet(resourceControllerTemplate, buildableTemplate);

        GalaxyManager.Planets.Add(newPlanet);

        GameObject planetRender = Instantiate(planetRenderPrefab);
        planetRender.GetComponent<PlanetRender>().PlanetData = newPlanet;
        planetRender.GetComponent<PlanetRender>().UpdateVariables();
    }
}
