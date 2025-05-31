using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawning : MonoBehaviour
{
    public GameObject StarWhite;
    public GameObject StarRed;
    public GameObject StarBlue;
    public GameObject StarPurple;

    public float starConcentration = 1000f;
    public float mapScale = 5f;

    private float randrangex;
    private float randrangey;
    private double numofstarwhite;
    private double numofstarred;
    private double numofstarblue;
    private double numofstarpurple;
    // Start is called before the first frame update
    void Start()
    {

        numofstarwhite = (5*starConcentration*mapScale);
        numofstarred = (0.7*starConcentration*mapScale);
        numofstarblue = (0.3*starConcentration*mapScale);
        numofstarpurple = (0.15*starConcentration*mapScale);

        randrangex = 225*mapScale;
        randrangey = 337*mapScale;

        //spawns stars as child of gameobject script is attached to

        //spawning white stars
        for (int i = 0 ; i < numofstarwhite ; i++) {Instantiate(StarWhite, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning red stars
        for (int i = 0 ; i < numofstarred ; i++) {Instantiate(StarRed, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning blue stars
        for (int i = 0 ; i < numofstarblue ; i++) {Instantiate(StarBlue, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        //spawning purple stars
        for (int i = 0 ; i < numofstarpurple ; i++) {Instantiate(StarPurple, new Vector2(Random.Range(-randrangex,randrangex),Random.Range(-randrangey,randrangey)), transform.rotation,transform);}
        
    }

}
