using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour, Idamageable
{

    protected Rigidbody2D rb;
    public float moveSpeed;
    public float sprintBoost = 1f;
    public int MaxHealth;
    protected int CurrentHealth;

    public MovePoint movePoint;

    public Room whatRoomImIn;

    public bool debugLogs;


    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        //finding movepoint
        movePoint = GetComponentInChildren<MovePoint>();
        //snaping to grid------------------------------------------------------------------------------
        Vector3 position = transform.position;
        transform.position = new Vector3 (Mathf.RoundToInt(position[0]-0.5f)+0.5f,Mathf.FloorToInt(position[1]),position[2]);
        //---------------------------------------------------------------------------------------------


    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        //always moving towards movepoint
        transform.position = Vector3.MoveTowards(transform.position,movePoint.transform.position,moveSpeed * sprintBoost * Time.deltaTime);
    }

/*
    public void Move2D(Vector3 direction, float sprintBoost) //takes a normalised vector as the argument
    {
        rb.velocity = direction*moveSpeed*sprintBoost;
    }
*/
    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
    }
}
