using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyRetreatState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}

    //what happens when you enter the state
    public override void EnterState()
    {
        Debug.Log("Enter Retreat State");
    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (!enemy.IsTooClose) {enemyStateMachine.ChangeState(enemy.CombatState);} //uses enemyStateMachine to change enemy to CombatState
        
        
    }

    public override void FixedFrameUpdate()
    {
        enemy.FaceAwayTarget(enemy.Target,enemy.rotationSpeed*2);
        enemy.MoveForward(40);
    }

    //what happens before exiting the state
    public override void ExitState()
    {
        Debug.Log("Exit Retreat State");
    }


}
