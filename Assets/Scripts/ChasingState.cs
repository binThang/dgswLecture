using System;
using UnityEngine;

public class ChasingState : EnemyState
{
    public ChasingState(Enemy enemy)
    {
        context = enemy;
    }

    public override void OnEnterState()
    {
        Debug.Log("On Enter Chaising");
    }

    public override void OnExitState()
    {
        Debug.Log("On Exit Chaising");
    }

    public override void PhysicsUpdateState()
    {
        if (Vector2.Distance(context.Target.transform.position,
                    context.transform.position) < context.attackRange)
        {
            context.rigid.velocity = new Vector2(0, 0);
            context.ChangeState(new IdleState());
            //state = EnemyState.Attack;
        }

        float moveDirection = context.Target.transform.position.x - context.transform.position.x;
        Vector2 moveForce = new Vector2(Mathf.Sign(moveDirection) * context.movePower, 0);

        context.rigid.AddForce(moveForce, ForceMode2D.Impulse);

        if (Mathf.Abs(context.rigid.velocity.x) > context.maxMoveSpeed)
            context.rigid.velocity = new Vector2(Mathf.Sign(moveDirection)
                * context.maxMoveSpeed, context.rigid.velocity.y);
    }

    public override void UpdateState()
    {
    }
}
