using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb Gun", menuName = "Weapons/Bomb Guns")]
public class BombGun : Weapon
{

    public int shotsLeft; //amo left
    private float weaponCoolDown; //how long between shots weapon cannot be used

    public override void Shoot(GameObject source)
    {

        weaponCoolDown = 3/fireRate; //calculating cooldown based on fire rate

        //wait for cooldown
        if (Time.time < lastShotTime + weaponCoolDown) return; //escapes methode if not enough time has passed

        //check for amo
        if (shotsLeft<=0) return; //escapes if no shots left
    
        GameObject Item = Instantiate(amo, source.transform.position+source.transform.up*2, source.transform.rotation); //spawnign in item
        Item.GetComponent<Bomb>().source = source; //sets source of item on item's script same as source of shot

        shotsLeft-=1;
        lastShotTime = Time.time;

    }
}
