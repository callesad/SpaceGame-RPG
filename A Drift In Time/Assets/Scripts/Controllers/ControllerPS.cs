using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPS: Controller
{
    PlayerShip player;

    public ControllerPS(PlayerShip player)
    {
        this.player = player;
    }

    public void GetMovement()
    {
        float speedBoost = 1f;
        float y = Input.GetAxis("Vertical");
        float x = -Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift)) speedBoost = 2.5f;

        player.UseThrusters(y,speedBoost);
        player.TurnShip(x);
    }

    public void GetAction()
    {
        if (Input.GetKey(KeyCode.Space))
            player.UseWeapon(player.weapon);

        if (Input.GetKey(KeyCode.Alpha1))
            player.ChangeWeapon(player.weaponList[0]);
        if (Input.GetKey(KeyCode.Alpha2))
            player.ChangeWeapon(player.weaponList[1]);
        if (Input.GetKey(KeyCode.Alpha3))
            player.ChangeWeapon(player.weaponList[2]);
    }
}
