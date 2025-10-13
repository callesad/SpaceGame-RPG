using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int damage;
    public string sourceTag;
    public float timeExploded;
    //HashSet<Collider2D> hitTargets = new HashSet<Collider2D>(); 
    
    // Start is called before the first frame update
    void Start()
    {
        timeExploded = Time.time;
    }

    void Update()
    {
        if (Time.time-timeExploded>0.4f) Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //Explosion hit condition
        if (!(target.CompareTag("Trigger")||target.CompareTag(sourceTag)||target.CompareTag("Amo"))) //if the object is not a trigger, not the same tag as the source, and not amo
            {
                if (target.TryGetComponent<Idamageable>(out Idamageable _damageable)){
                    _damageable.Damage(damage);
                    //hitTargets.Add(target);    
                }
                
            }     
    }
}
