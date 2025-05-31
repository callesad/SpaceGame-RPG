using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyPursueState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}

    //what happens when you enter the state
    public override void EnterState()
    {
        Debug.Log("Enter Pursue State");
    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (!enemy.IsTooFar) {enemyStateMachine.ChangeState(enemy.CombatState);} //uses enemyStateMachine to change enemy to CombatState
        
        
    }

    public override void FixedFrameUpdate()
    {
        enemy.FaceTarget(enemy.Target,enemy.rotationSpeed);
        enemy.MoveForward(40);
    }

    //what happens before exiting the state
    public override void ExitState()
    {
        Debug.Log("Exit Pursue State");
    }


}