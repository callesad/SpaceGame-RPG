using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
   
    //movement variables
    private Rigidbody2D rb;
    public float boosterStrength = 15f;
    public float rotationStrength = 15f;

    //weapon variables
    public GameObject weaponList; //List of Player Weapons
    public int weaponindex = 0;
    private ShipWeapons weaponScript; //script from weapon
   
    //player controller
    PlayerController controller;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponScript = weaponList.GetComponent<ShipWeapons>();
        weaponScript.lastShotTime=0; //reset lastDhotTime because otherwise it carries on from last instance and code breaks
        controller = new PlayerController(this,rb,boosterStrength,rotationStrength,weaponScript);
    }

    void Update()
    {
        controller.GetAction();
        // Switch weapons using number keys
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    weaponindex = 0;
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    weaponindex = 1;

        //shoot button
        //if (Input.GetKey(KeyCode.Space)) {
        //    weaponScript.Shoot(this.gameObject,weaponindex);
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.GetMovement();
        //wasd controles
        //if (Input.GetKey(KeyCode.A)) {rb.AddTorque(rotationStrength);} //turn left
        //if (Input.GetKey(KeyCode.D)) {rb.AddTorque(-rotationStrength);} //turn right
        //if (Input.GetKey(KeyCode.W)) {//use thrusters
        //    if(Input.GetKey(KeyCode.LeftShift)) {rb.AddForce(transform.up*boosterStrength*2.5f);} //use thrusters with boost
        //    else {rb.AddForce(transform.up*boosterStrength);}} //use thrusters without boost
        //if (Input.GetKey(KeyCode.S)) {rb.AddForce(-transform.up*boosterStrength/2);} //reverse thrusters
    }
}
