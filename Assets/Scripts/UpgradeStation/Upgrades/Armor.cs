using UnityEngine;

public class Armor : Upgrade
{
    [SerializeField] private int _value;

    public override void Activate()
    {
        Player.SetArmor(_value);
    } 
}
