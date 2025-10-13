using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : SpaceShipBase//, Ispawnable
{

    private EnemyStateMachine stateMachine; //state machine
    

    //spawning info
    //public EnemySpawner spawner;
    //Ispawnable Variables
    //public bool spawned { get; set; } = false;
    //public bool permisionToDespawn { get; set; } = false;

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

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine(); //initializing state machine

        //initializing states using constructors
        IdolState = new EnemyIdolState(this,stateMachine);
        CombatState = new EnemyCombatState(this,stateMachine);
        PursueState = new EnemyPursueState(this,stateMachine);
        RetreatState = new EnemyRetreatState(this,stateMachine);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(IdolState); //enter idol state

    }

    void Update()
    {
        if (Target==null&&IsAgroed) IsAgroed = false; //if target disapears switches IsAgroed to false
        stateMachine.currentEnemyState.FrameUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.currentEnemyState.FixedFrameUpdate(); //calls the frame update methode from the current state from the state machine
    }

    //Ispawnable
    //--public void Despawn() 
    //--{
    //--    Destroy(this.gameObject);
    //--    if (spawner!=null) spawner.spawned=false;
    //--}

    //--public void Spawn(){
        //there is no spawn
    //--}

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
}
