using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{

    //player reference
    protected PlayerScript player;

    //movement variables
    protected Rigidbody2D rb;
    protected float boosterStrength;
    protected float rotationStrength;

    //weapon reference
    protected ShipWeapons weaponScript; 

    //constructor
    public PlayerController(PlayerScript player, Rigidbody2D rb, float boosterStrength, float rotationStrength, ShipWeapons weaponScript)
    {
        this.player = player;
        this.rb = rb;
        this.boosterStrength = boosterStrength;
        this.rotationStrength = rotationStrength;
        this.weaponScript = weaponScript;
    }

    // Update is called once per frame
    public void GetAction()
    {
        // Switch weapons using number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            player.weaponindex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            player.weaponindex = 1;

        //shoot button
        if (Input.GetKey(KeyCode.Space)) {
            weaponScript.Shoot(player.gameObject,player.weaponindex);
        }
    }

    public void GetMovement()
    {
        //wasd controles
        if (Input.GetKey(KeyCode.A)) {rb.AddTorque(rotationStrength);} //turn left
        if (Input.GetKey(KeyCode.D)) {rb.AddTorque(-rotationStrength);} //turn right
        if (Input.GetKey(KeyCode.W)) {//use thrusters
            if(Input.GetKey(KeyCode.LeftShift)) {rb.AddForce(player.transform.up*boosterStrength*2.5f);} //use thrusters with boost
            else {rb.AddForce(player.transform.up*boosterStrength);}} //use thrusters without boost
        if (Input.GetKey(KeyCode.S)) {rb.AddForce(-player.transform.up*boosterStrength/2);} //reverse thrusters
    }
}

