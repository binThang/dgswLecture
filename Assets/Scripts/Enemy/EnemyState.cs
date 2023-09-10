using System;
public abstract class EnemyState
{
    public Enemy context;

    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void UpdateState();
    public abstract void PhysicsUpdateState();
}
