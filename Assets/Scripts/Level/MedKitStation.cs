using UnityEngine;

public class MedKitStation : MonoBehaviour
{
    public int Price { get; private set; } = 15;

    private Wallet _wallet;
    private Player _player;

    public bool TrySellMedKit()
    {
        if (_wallet.TryDecreaseCollectedMoney(Price))
        {
            _wallet.DecreaseCollectedMoney(Price);
            _player.BuyMedKit();
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = player;
        }

        if (collision.TryGetComponent(out PhysicsMovement physicsMovement))
        {
            if (_player.NeedHeal())
            {
                physicsMovement.TryToMove(false);
                _player.ActivateMedKitScreen(this);
            }           
        }     

        if (collision.TryGetComponent(out Wallet wallet))
        {
            _wallet = wallet;
        }
    }
}