using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Idamageable, Ispawnable
{

    private Rigidbody2D rb;

    public int MaxHealth = 50; //max health points
    private int CurrentHealth; //current remaining health points

    private EnemyStateMachine stateMachine; //state machine

    //spawning info
    public EnemySpawner spawner;
    //Ispawnable Variables
    public bool spawned { get; set; } = false;
    public bool permisionToDespawn { get; set; } = false;

    //Declaring state variables
    public EnemyIdolState IdolState;
    public EnemyCombatState CombatState;
    public EnemyPursueState PursueState;
    public EnemyRetreatState RetreatState;

    //Trigger bools
    public bool IsAgroed = false;
    public bool IsTooClose = false;
    public bool IsTooFar = true;

    //reference to target
    public GameObject Target;

    //movement values
    public float boostforce;
    public float rotationSpeed;

    //weapon
    public GameObject weaponList;
    public int weaponindex = 0;
    EnemyWeapons weaponScript;
    public float lastShotTime; //last time weapon was fired

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //initializing weapon variables
        weaponScript = weaponList.GetComponent<EnemyWeapons>();
        lastShotTime=0; //reset lastDhotTime because otherwise it carries on from last instance and code breaks

        stateMachine = new EnemyStateMachine(); //initializing state machine

        //initializing states using constructors
        IdolState = new EnemyIdolState(this,stateMachine);
        CombatState = new EnemyCombatState(this,stateMachine);
        PursueState = new EnemyPursueState(this,stateMachine);
        RetreatState = new EnemyRetreatState(this,stateMachine);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth; //resets health to max health
        stateMachine.Initialize(IdolState); //enter idol state

    }

    void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.currentEnemyState.FixedFrameUpdate(); //calls the frame update methode from the current state from the state machine
    }






    //local functions
    //-------------------------------------------
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage; //reduces health by damage amount

        if (CurrentHealth <= 0){Die();} //activates death if health is below zero
    }

    void Death()
    {
        Destroy(this.gameObject);
    }



    //Ispawnable
    public void Despawn() 
    {
        Destroy(this.gameObject);
        if (spawner!=null) spawner.spawned=false;
    }

    public void Spawn(){
        //there is no spawn
    }





    public void FaceTarget(GameObject target, float rotationSpeed)
    {
    Vector2 direction = target.transform.position - transform.position;

    // Calculate the angle in degrees
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; //finds angle in radians between x axis and direction of object then converts to degrees
                                                                              //Mathf.Atan2 uses transform.right, so must subtract 90 to adjust
    // Create a rotation only around the Z axis
    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

    transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRotation,
        rotationSpeed * Time.deltaTime
        );
    }

    public void FaceAwayTarget(GameObject target, float rotationSpeed)
    {
    Vector2 direction = target.transform.position - transform.position;

    // Calculate the angle in degrees
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90; //finds angle in radians between x axis and direction of object then converts to degrees
                                                                              //Mathf.Atan2 uses transform.right, so must subtract 90 to adjust
    // Create a rotation only around the Z axis
    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

    transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRotation,
        rotationSpeed * Time.deltaTime
        );
    }

    public void MoveForward(float multiplier)
    {
        rb.AddForce(transform.up*boostforce*multiplier);
    }





    public void ShootWeapon()
    {
        weaponScript.Shoot(this.gameObject,weaponindex);
    }




    //Idamageable interface implimentations 
    public void Damage(int damage){TakeDamage(damage);}
    public void Die(){Death();}


}
