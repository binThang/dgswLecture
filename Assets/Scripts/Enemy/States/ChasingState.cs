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
        context.SetTriggerAnimation("Run");
        //Debug.Log("On Enter Chaising");
    }

    public override void OnExitState()
    {
        context.rigid.velocity = Vector2.zero;
        //Debug.Log("On Exit Chaising");
    }

    public override void PhysicsUpdateState()
    {
        if (Mathf.Abs(context.rigid.velocity.x) > context.maxMoveSpeed)
            context.rigid.velocity = new Vector2(Mathf.Sign(context.rigid.velocity.x)
                * context.maxMoveSpeed, context.rigid.velocity.y);
    }

    public override void UpdateState()
    {
        // 추격
        float moveDirection = context.Target.transform.position.x - context.transform.position.x;
        Vector2 moveForce = new Vector2(Mathf.Sign(moveDirection) * context.movePower, 0);
        context.rigid.AddForce(moveForce);

        // 거리 판단
        var distance = Vector2.Distance(context.Target.transform.position, context.transform.position);

        // 공격
        if (distance < context.attackRange) 
        {
            context.rigid.velocity = new Vector2(0, 0);
            context.ChangeState(new AttackState(context));
            //state = EnemyState.Attack;
        }

        // 추격 포기
        if (distance > 10f)
        {
            context.ChangeState(new IdleState(context));
        }
    }
}
