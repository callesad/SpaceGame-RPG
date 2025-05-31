using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnLazerBehaviour : MonoBehaviour
{

    public GameObject source; //source of lazer
    private GameObject player; //player character
    private Rigidbody2D rb; //lazer rigid body
    private Rigidbody2D srb; //source rigid body

    public float lazer_speed = 20;
    public int damage;

    private Vector3 positionDiff; //distance from player

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        srb = source.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * lazer_speed + new Vector3 (srb.velocity.x,srb.velocity.y,0);
    }

    // Update is called once per frame
    void Update()
    {
        positionDiff = transform.position - player.transform.position;
        if (positionDiff.sqrMagnitude>=300) {Destroy(gameObject);}
    }

    void OnTriggerEnter2D(Collider2D target)
    {
    
        //if (target.TryGetComponent<Enemy>(out Enemy enemyScript)){enemyScript.Damage(damage);} //if target has enemyCore damages enemy
    
        if (target.TryGetComponent<Idamageable>(out Idamageable _damageable)&&!target.CompareTag("Enemy")){_damageable.Damage(damage);}

        if (!target.CompareTag("Trigger")&&!target.CompareTag("Enemy")) {Destroy(gameObject);} //despawn lazer

//--------debugging-----------------------------------------------------------------------------------------------//
        if (!target.CompareTag("Trigger")&&!target.CompareTag("Enemy")) Debug.Log("lazer hit " + target);         //
//----------------------------------------------------------------------------------------------------------------//
        
    }

}
