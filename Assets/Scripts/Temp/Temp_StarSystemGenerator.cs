using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_StarSystemGenerator : MonoBehaviour
{
    int MaxConnections = 3;
    int MaxStarSystems = 10;
    int CreatedStarSystems = 0;
    [SerializeField] GameObject StarSystemRenderPrefab;
    [SerializeField] InteractableTemplate[] interactableTemplates;
    [SerializeField] GameObject StarSystemConnectionPrefab;

    GameManager GameManager = null;

    StarSystemRender[] StarSystems = new StarSystemRender[25];

    public void Start()
    {
        GameManager = FindObjectOfType<GameManager>();

        for(int i = 0; i < 25; i++)
        {
            GenerateStarSystem();
        }

        PositionStarSystems();
        ConnectStarSystems();
        
    }

    void GenerateStarSystem()
    {

        StarSystem CreatedStarSystem = new StarSystem();
        GameManager.RegisterStarSystem(CreatedStarSystem);

        foreach (InteractableTemplate interactableTemplate in interactableTemplates)
        {
            switch (interactableTemplate.Type)
            {
                case EInteractableType.Planet:
                    GeneratePlanet(CreatedStarSystem, interactableTemplate);
                    break;
                default:
                    break;
            }
        }

        SpawnStarSystem(CreatedStarSystem);

    }

    void GeneratePlanet(StarSystem _createdStarSystem, InteractableTemplate _template)
    {
        Temp_PlanetGenerator pg = FindObjectOfType<Temp_PlanetGenerator>();
        if(pg != null)
        {
            Planet newPlanet = pg.GeneratePlanet(_template);
            _createdStarSystem.Contents.Add(newPlanet);
        }
    }

    void SpawnStarSystem(StarSystem _createdStarSystem)
    {
        GameObject go = Instantiate(StarSystemRenderPrefab);
        go.transform.position = new Vector3(-200 - (50 * CreatedStarSystems), 0, -2);
        StarSystemRender sr = go.GetComponent<StarSystemRender>();
        if(sr != null)
        {
            sr.SetData(_createdStarSystem);
        }

        StarSystems[CreatedStarSystems] = sr;
        _createdStarSystem.StarSystemRender = sr;
        CreatedStarSystems++;
    }

    void PositionStarSystems()
    {
        float xOffset = 0;
        float yOffset = 0;
        for(int i = 0; i < 25; i++)
        {
            if(xOffset == 5)
            {
                xOffset = 0;
                yOffset++;
            }

            StarSystems[i].transform.position = new Vector3(-500 + (50 * xOffset), -50 * yOffset, -2);

            xOffset++;

        }
    }
    void ConnectStarSystems()
    {
        float xCount = 0;
        for (int i = 0; i < 25; i++)
        {
            if (xCount == 5)
            {
                xCount = 0;
            }
            if (xCount < 4)
            {
                StarSystemConnection con = new StarSystemConnection(StarSystems[i].GetStarSystemData(), StarSystems[i + 1].GetStarSystemData());
                StarSystems[i].GetStarSystemData().AddConnection(con);
                StarSystems[i + 1].GetStarSystemData().AddConnection(con);
                
            }
            

            if(i + 5 < 25)
            {
                StarSystemConnection con = new StarSystemConnection(StarSystems[i].GetStarSystemData(), StarSystems[i + 5].GetStarSystemData());
                StarSystems[i].GetStarSystemData().AddConnection(con);
                StarSystems[i + 5].GetStarSystemData().AddConnection(con);
            }

            xCount++;
        }

        SpawnConnections();
    }
    void SpawnConnections()
    {
        for(int i = 0; i < 25; i++)
        {
            foreach(StarSystemConnection con in StarSystems[i].GetStarSystemData().Connections)
            {
                if(!con.IsInitialized)
                {
                    Vector3 dir = con.StarSystemB.StarSystemRender.transform.position - con.StarSystemA.StarSystemRender.transform.position;
                    float dist = Vector3.Distance(con.StarSystemA.StarSystemRender.transform.position, con.StarSystemB.StarSystemRender.transform.position);
                    Vector3 pos = con.StarSystemA.StarSystemRender.transform.position + dir.normalized * (dist / 2);

                    GameObject go = Instantiate(StarSystemConnectionPrefab);
                    go.transform.position = new Vector3(pos.x, pos.y, -1);

                    LineRenderer lr = go.GetComponent<LineRenderer>();
                    Vector3 posA = go.transform.InverseTransformPoint(con.StarSystemA.StarSystemRender.transform.position);
                    Vector3 posB = go.transform.InverseTransformPoint(con.StarSystemB.StarSystemRender.transform.position);
                    lr.SetPosition(0, new Vector3(posA.x, posA.y, 0));
                    lr.SetPosition(1, new Vector3(posB.x, posB.y, 0));

                    con.IsInitialized = true;
                }
            }
        }
    }
}
