using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _isElectroTrap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage, _isElectroTrap);
        }
    }
}