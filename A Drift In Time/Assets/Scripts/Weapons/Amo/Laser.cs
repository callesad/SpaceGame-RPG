using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Amo
{
    public int damage;

    void OnTriggerEnter2D(Collider2D target)
    {
    
        //if (target.TryGetComponent<Enemy>(out Enemy enemyScript)){enemyScript.Damage(damage);} //if target has enemyCore damages enemy

        //laser hit condition
        //does not collide with self, triggers, amo, or explosion
        if (!(target.CompareTag("Trigger")||target.CompareTag(sourceTag)||target.CompareTag("Amo")||target.CompareTag("Explosion"))) //if the object is not a trigger, not the same tag as the source, and not amo
            {
                if (target.TryGetComponent<Idamageable>(out Idamageable _damageable)){_damageable.Damage(damage);}
                Destroy(gameObject); //despawn lazer
            } 

//--------debugging-----------------------------------------------------------------------------------------------------------------------------------------//
        //if (!(target.CompareTag("Trigger")||target.CompareTag(sourceTag)||target.CompareTag("Amo")))  Debug.Log("lazer hit " + target);         //
//----------------------------------------------------------------------------------------------------------------------------------------------------------//
        
    }

}
