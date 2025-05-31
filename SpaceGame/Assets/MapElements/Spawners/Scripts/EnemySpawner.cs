using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, Ispawnable
{

    public GameObject EnemyPreFab;
    //private GameObject RenderRadius;
    private Enemy EnemyScript;
    //Ispawnable Variables
    public bool spawned { get; set; } = false;
    public bool permisionToDespawn { get; set; } = false;


    // Start is called before the first frame update
    void Awake()
    {
        //RenderRadius = GameObject.FindGameObjectWithTag("Player").transform.Find("RenderRadius").gameObject; //gets reference to RenderRadius of player
        EnemyScript = EnemyPreFab.GetComponent<Enemy>(); //gets reference to Enemy's script
    }

    public void Spawn()
    {
        EnemyScript.spawner = this;
        Instantiate(EnemyPreFab,transform.position + transform.up*3f,transform.rotation);
        Instantiate(EnemyPreFab,transform.position + transform.right*2.5f ,transform.rotation);
        Instantiate(EnemyPreFab,transform.position - transform.right*2.5f ,transform.rotation);

        spawned = true;
    }

    public void Despawn(){
        //there is no despawn in bahsingsaiy
    }

}
