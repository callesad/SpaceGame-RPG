using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawning : MonoBehaviour
{

    public GameObject BonianFleetSpawner;
    public GameObject AsteroidSpawner;

    public int numofAsteroids = 1000;
    public int numofBonianFleets = 400;

    // Start is called before the first frame update
    void Start()
    {
        //spawning Bonian Fleets
        for (int i = 0 ; i < numofBonianFleets ; i++) {Instantiate(BonianFleetSpawner, new Vector2(Random.Range(-650,650),Random.Range(-1300,1300)), transform.rotation,transform);}
        //spawning Asteroids
        for (int i = 0 ; i < numofAsteroids ; i++) {Instantiate(AsteroidSpawner, new Vector2(Random.Range(-650,650),Random.Range(-1300,1300)), transform.rotation,transform);}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
