using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Amo
{
    private float timeFired;
    public int explosionDamage = 10;
    public float explosionScale = 1f;
    public GameObject explosionPreFab;
    GameObject explosion = null;

    void Detonate()
    {
        if (explosionPreFab!=null) explosion = Instantiate(explosionPreFab, transform.position, Quaternion.identity);
        if (explosion.TryGetComponent<Explosion>(out Explosion explosionScript))
        {
            explosionScript.damage = explosionDamage;
            explosionScript.sourceTag = sourceTag;
            explosion.transform.localScale *= explosionScale;
        }

        Destroy(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        timeFired = Time.time;

    }

    protected override void Update()
    {
        if (player==null) 
        {
            Destroy(gameObject);
            return;
        }
        if (Time.time-timeFired>1.1f) Detonate();

    }

}


