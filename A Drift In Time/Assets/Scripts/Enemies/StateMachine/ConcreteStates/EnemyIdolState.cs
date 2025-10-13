using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdolState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyIdolState(EnemyShip enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}


    //has enemy script
    //has enemyStateMachine script
    

    //what happens when you enter the state
    public override void EnterState()
    {
       
        //--enemy.permisionToDespawn = true;
    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (enemy.IsAgroed) {enemyStateMachine.ChangeState(enemy.CombatState);} //uses enemyStateMachine to change enemy to CombatState
        
    }

    public override void FixedFrameUpdate()
    {
        enemy.UseThrusters(1,0.3f);

    }

    //what happens before exiting the state
    public override void ExitState()
    {
        
        //--enemy.permisionToDespawn = false;
    }


}
