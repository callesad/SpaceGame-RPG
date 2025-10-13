using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    GameObject player;
    public float paralaxFactor; //bigger number make paralax slower

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        paralaxFactor += 1;
    }

    // Update is called once per frame
    void Update()
    { 
        if (player!=null) transform.position = (paralaxFactor)*player.transform.position-player.transform.position;
    }
}
