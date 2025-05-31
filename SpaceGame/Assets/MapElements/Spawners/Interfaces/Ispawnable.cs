using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ispawnable
{
    void Despawn();
    void Spawn();

    bool spawned { get; }
    bool permisionToDespawn { get; }
    //only despawn if permission to despawn is true
}
