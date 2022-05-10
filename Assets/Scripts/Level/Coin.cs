using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet.AddCoin(_value);           
            gameObject.SetActive(false);
        }
    }  
}