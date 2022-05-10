using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private string _label;

    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private bool _needExtraBar;

    private Player _player;
    private PhysicsMovement _physicsMovement;

    public PhysicsMovement PhysicsMovement => _physicsMovement;
    public Player Player => _player;
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool NeedExtraBar => _needExtraBar;

    public abstract void Activate();

    public void Init(Player player, PhysicsMovement physicsMovement)
    {
        _player = player;
        _physicsMovement = physicsMovement;
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}