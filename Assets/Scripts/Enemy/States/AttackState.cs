using System;
using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    float attackDelay = 0.5f;

    public AttackState(Enemy enemy)
    {
        context = enemy;
    }

    public override void OnEnterState()
    {
        context.rigid.velocity = Vector2.zero;
        context.StartCoroutine(AttackAfterDelay());
    }

    public override void OnExitState()
    {
    }

    public override void PhysicsUpdateState()
    {
    }

    public override void UpdateState()
    {
    }

    private IEnumerator AttackAfterDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        // Attack Animation
        context.SetTriggerAnimation("Attack");

        yield return new WaitForSeconds(0.5f);

        // Damage to player
        context.Attack();

        yield return new WaitForSeconds(1.5f);
        context.ChangeState(new IdleState(context));
    }
}
