using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyState //inherits from EnemyState
{

    //defining the EnemyIdolState constructor and passing its values to the EnemyState constructor from the base class EnemyEtate
    public EnemyCombatState(EnemyShip enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine){}

    //what happens when you enter the state
    public override void EnterState()
    {

    }

    //what heppens each frame while in the state
    public override void FrameUpdate()
    {
        if (enemy.IsTooClose) {enemyStateMachine.ChangeState(enemy.RetreatState);} //uses enemyStateMachine to change enemy to RetreatState
        if (!enemy.IsAgroed) {enemyStateMachine.ChangeState(enemy.IdolState);} //uses enemyStateMachine to change enemy to IdolState
        if (enemy.IsTooFar) {enemyStateMachine.ChangeState(enemy.PursueState);} //uses enemyStateMachine to change enemy to PursueState
    
        enemy.UseWeapon(enemy.weapon);
    }

    public override void FixedFrameUpdate()
    {
        enemy.FaceTarget(enemy.Target,enemy.turnSpeed);
    }

    //what happens before exiting the state
    public override void ExitState()
    {
    
    }


}
