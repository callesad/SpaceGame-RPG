using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyPursueState(EnemyShip enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}

    //what happens when you enter the state
    public override void EnterState()
    {
        
    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (!enemy.IsAgroed) enemyStateMachine.Initialize(enemy.IdolState); //switches to idol if enemy is unagroed
        if (!enemy.IsTooFar) enemyStateMachine.ChangeState(enemy.CombatState); //uses enemyStateMachine to change enemy to CombatState
        
        
    }

    public override void FixedFrameUpdate()
    {
        if (enemy.Target!=null) enemy.FaceTarget(enemy.Target,enemy.turnSpeed);
        enemy.UseThrusters(1);
    }

    //what happens before exiting the state
    public override void ExitState()
    {
        
    }


}