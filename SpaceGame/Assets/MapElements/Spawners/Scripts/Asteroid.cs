using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, Ispawnable
{


    //spawning info
    public AsteroidSpawner spawner;
    //Ispawnable Variables
    public bool spawned { get; set; } = true;
    public bool permisionToDespawn { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn()
    {
        //there is no Spawn in bahsingsai
    }

    public void Despawn() 
    {
        Destroy(this.gameObject);
        if (spawner!=null) spawner.spawned=false;
    }
}
