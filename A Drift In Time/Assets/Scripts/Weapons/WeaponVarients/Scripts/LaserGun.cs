using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser Gun", menuName = "Weapons/Laser Guns")]
public class LaserGun : Weapon
{

    private float weaponCoolDown; //how long between shots weapon cannot be used

    public override void Shoot(GameObject source)
    {
        weaponCoolDown = 3/fireRate; //calculating cooldown based on fire rate

        //wait for cooldown
        if (Time.time < lastShotTime + weaponCoolDown){return;} //escapes methode if not enough time has passed

        GameObject laser = Instantiate(amo, source.transform.position+source.transform.up*2, source.transform.rotation); //spawnign in bullet
        laser.GetComponent<Laser>().source = source; //sets source of laser on laser's script same as source of shot

        lastShotTime = Time.time;
    }
}
