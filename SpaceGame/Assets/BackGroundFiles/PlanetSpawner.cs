using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public GameObject planet4;

    public float planetConcentration = 1000f;
    public float mapScale = 5f;

    private float randrangex;
    private float randrangey;
    private double numofplanet1;
    private double numofplanet2;
    private double numofplanet3;
    private double numofplanet4;
    // Start is called before the first frame update
    void Start()
    {

        numofplanet1 = (0.4*planetConcentration*mapScale);
        numofplanet2 = (0.3*planetConcentration*mapScale);
        numofplanet3 = (0.3*planetConcentration*mapScale);
        numofplanet4 = (0.15*planetConcentration*mapScale);

        randrangex = 225*mapScale;
        randrangey = 337*mapScale;

        //spawns stars as child of gameobject script is attached to

        //spawning white stars
        for (int i = 0 ; i < numofplanet1 ; i++) {Instantiate(planet1, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning red stars
        for (int i = 0 ; i < numofplanet2 ; i++) {Instantiate(planet2, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning blue stars
        for (int i = 0 ; i < numofplanet3 ; i++) {Instantiate(planet3, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning purple stars
        for (int i = 0 ; i < numofplanet4 ; i++) {Instantiate(planet4, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        
    }

}
