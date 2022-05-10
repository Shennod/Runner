using UnityEngine;

public class HealthUp : Upgrade
{
    [SerializeField] private int _value;
    public override void Activate()
    {
        Player.SetHealth(_value);
    }   
}