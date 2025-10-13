using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyRetreatState(EnemyShip enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}

    //what happens when you enter the state
    public override void EnterState()
    {
        
    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (!enemy.IsAgroed) enemyStateMachine.Initialize(enemy.IdolState); //switches to idol if enemy is unagroed
        if (!enemy.IsTooClose) enemyStateMachine.ChangeState(enemy.CombatState); //uses enemyStateMachine to change enemy to CombatState
        
    
    }

    public override void FixedFrameUpdate()
    {
        enemy.FaceAwayTarget(enemy.Target,enemy.turnSpeed*2);
        enemy.UseThrusters(1,2f);
    }

    //what happens before exiting the state
    public override void ExitState()
    {
      
    }


}
