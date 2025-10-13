using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon: ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public GameObject amo;
    public float lastShotTime; //last time weapon was fired

    public abstract void Shoot(GameObject owner);
}
