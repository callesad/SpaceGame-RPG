using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D subject)
    {
        if (subject.TryGetComponent<Ispawnable>(out Ispawnable _spawnable)&&!_spawnable.spawned){_spawnable.Spawn();}
    }

    void OnTriggerExit2D(Collider2D subject)
    {
        if (subject.TryGetComponent<Ispawnable>(out Ispawnable _spawnable)&&_spawnable.permisionToDespawn){_spawnable.Despawn();}
    }
}
