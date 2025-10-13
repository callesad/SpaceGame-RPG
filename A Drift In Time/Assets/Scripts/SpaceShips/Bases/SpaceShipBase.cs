using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipBase : MonoBehaviour, Idamageable
{

    protected Rigidbody2D rb;

    [Header("Weapons")]
    public Weapon[] weaponList;
    public Weapon weapon;

    public int MaxHealth = 50; //max health points
    [SerializeField]
    protected int CurrentHealth; //current remaining health points

    public int boosterStrength;
    public int turnSpeed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (weaponList[0]!=null)
        {
            weapon = ScriptableObject.Instantiate(weaponList[0]); //makes a copy of the scriptable object tha the weapon data is on
            weapon.lastShotTime = Time.time; //if there is a weapon equipted resets the lastShotTime variable
        }
        CurrentHealth = MaxHealth; //resets health to max health
    }

    protected virtual void Start()
    {
        
    }

    public void UseThrusters(float direction, float magnitude = 1f)
    {
        rb.AddForce(this.transform.up*direction*magnitude*boosterStrength);
    }

    public void TurnShip(float direction)
    {
        rb.AddTorque(direction*turnSpeed);
    }

    public void UseWeapon(Weapon weapon)
    {
        weapon.Shoot(this.gameObject);
    }

    protected void Explode()
    {
        Destroy(this.gameObject);
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (newWeapon!=null)
        {
            weapon = ScriptableObject.Instantiate(newWeapon); //makes a copy of the scriptable object tha the weapon data is on
            weapon.lastShotTime = Time.time; //if there is a weapon equipted resets the lastShotTime variable
        }
        else weapon = null;

    }


    
    //Idamageable interface implimentations 
    //-------------------------------------------
    public virtual void Damage(int damage)
    {
        CurrentHealth -= damage; //reduces health by damage amount

        if (CurrentHealth <= 0){Die();} //activates death if health is below zero
    }

    public void Die()
    {
        Explode();
    }

}
