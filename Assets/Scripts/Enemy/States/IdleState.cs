using System;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy enemy)
    {
        context = enemy;
    }

    public override void OnEnterState()
    {
        context.rigid.velocity = Vector2.zero;
    }

    public override void OnExitState()
    {
    }

    public override void PhysicsUpdateState()
    {
    }

    public override void UpdateState()
    {
        if (context.Target == null)
            return;

        if (Vector2.Distance(context.Target.transform.position, context.transform.position) < context.findRange)
            context.ChangeState(new ChasingState(context));
    }
}
