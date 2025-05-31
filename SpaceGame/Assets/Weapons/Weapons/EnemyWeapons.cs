using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{

    public Weapon[] EnemyWeaponList; //list of weapons

    //public float lastShotTime; //last time weapon was fired
    private float weaponCoolDown; //how long between shots weapon cannot be used

    //needs the source GameObject as argument
    public void Shoot(GameObject source,int index){

        weaponCoolDown = 3/EnemyWeaponList[index].fireRate; //calculating cooldown based on fire rate

        //wait for cooldown
        if (Time.time < source.GetComponent<Enemy>().lastShotTime + weaponCoolDown){return;} //escapes methode if not enough time has passed

        GameObject lazer = Instantiate(EnemyWeaponList[index].amo, source.transform.position+source.transform.up*2, source.transform.rotation); //spawnign in bullet
        lazer.GetComponent<EnLazerBehaviour>().source = source; //sets source of lazer on lazer's script same as source of shot

        source.GetComponent<Enemy>().lastShotTime = Time.time; //records time weapon was fired


        
    }


}
