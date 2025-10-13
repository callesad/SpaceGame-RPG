using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amo : MonoBehaviour
{
    public GameObject source; //source of laser
    protected GameObject player; //player
    protected Rigidbody2D rb; //lazer rigid body
    protected Rigidbody2D srb; //source rigid body
    protected string sourceTag; //tag of source

    public float amo_speed = 20;

    protected Vector3 positionDiff; //distance from source

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (source==null) Destroy(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        srb = source.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * amo_speed + new Vector3 (srb.velocity.x,srb.velocity.y,0); 
        sourceTag = source.gameObject.tag;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player==null) 
        {
            Destroy(gameObject);
            return;
        }
        //there is a better way to write this with unity methodes 
        positionDiff = transform.position - player.transform.position;
        if (positionDiff.sqrMagnitude>=420) Destroy(gameObject);
    }

}
