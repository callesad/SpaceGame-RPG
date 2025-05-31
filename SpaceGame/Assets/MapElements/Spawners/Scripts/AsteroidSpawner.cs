using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour, Ispawnable
{

    public GameObject AsteroidPreFab;
    private Asteroid AsteroidScript;
    //Ispawnable Variables
    public bool spawned { get; set; } = false;
    public bool permisionToDespawn { get; set; } = false;

    void Awake()
    {
        AsteroidScript = AsteroidPreFab.GetComponent<Asteroid>();
    }

    public void Spawn()
    {
        AsteroidScript.spawner = this;
        Instantiate(AsteroidPreFab,transform.position,transform.rotation);

        spawned = true;
    }

    public void Despawn()
    {
        //there is no Despawn in bahsingsai
    }
}
