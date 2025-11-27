using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{

    protected Rigidbody2D rb;
    public float moveSpeed;
    public float sprintBoost = 1f;

    public MovePoint movePoint;

    public Room whatRoomImIn;

    public bool debugLogs;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //finding movepoint
        movePoint = GetComponentInChildren<MovePoint>();
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
 
}
