using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsidePlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += (Vector2)transform.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection -= (Vector2)transform.up;
        if (Input.GetKey(KeyCode.D))
            moveDirection += (Vector2)transform.right;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= (Vector2)transform.right;

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
