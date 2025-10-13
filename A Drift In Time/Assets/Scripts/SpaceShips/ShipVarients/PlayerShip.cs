using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : SpaceShipBase
{

    ControllerPS controller;
    public HealthBar healthBar;

    protected override void Awake()
    {
        base.Awake();
        controller = new ControllerPS(this);
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (healthBar!=null) healthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.GetMovement();
    }

    void Update()
    {
        controller.GetAction();
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        healthBar.SetHealth(CurrentHealth);
    }
}
