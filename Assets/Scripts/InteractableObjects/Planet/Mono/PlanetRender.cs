using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRender : MonoBehaviour
{

    public Planet PlanetData { get; set; }

    public List<ResourceInOut> Inputs = new List<ResourceInOut>();
    public List<ResourceInOut> Outputs = new List<ResourceInOut>();
    public List<EFacility> CompatibleFacilities = new List<EFacility>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVariables()
    {
        Inputs = PlanetData.Inputs;
        Outputs = PlanetData.Outputs;
        CompatibleFacilities = PlanetData.CompatibleFacilities;
    }
}
