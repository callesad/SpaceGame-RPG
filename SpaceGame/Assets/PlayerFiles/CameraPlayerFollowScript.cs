//I use AddForce so that the camera trails ahead of the player and resets the for condition to false

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowScript : MonoBehaviour
{

    private GameObject player;
    private Vector3 positionDiff;
    private Rigidbody2D rb;
    private float followSpeed;
    private PlayerScript pc;
    public bool cameraFollowDelay = false;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerScript>();
        followSpeed = 15f * pc.boosterStrength/15f; //scales the follow speed with the speed of the ship
    }


    // Update is called once per frame
    void Update()
    {
        //if (cameraFollowDelay) {
        //finging the difference between the position of the camera and position of the player
        //positionDiff = transform.position - player.transform.position;
       
        //using sqrMagnitude for computational effeciency
        //    if(positionDiff.sqrMagnitude-100>=9){rb.AddForce(-positionDiff.normalized*followSpeed);}

        //}else {
        //just a simple camera locking script without rotation
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        //}
    }
}
