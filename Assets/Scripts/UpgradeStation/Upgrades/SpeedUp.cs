using UnityEngine;

public class SpeedUp : Upgrade
{
    [SerializeField] private int _speed;

    public override void Activate()
    {
        PhysicsMovement.SetMoveSpeed(_speed);
    }
}