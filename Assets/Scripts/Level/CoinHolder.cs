using System.Collections.Generic;
using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    private List<GameObject> _coins = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _coins.Add(transform.GetChild(i).gameObject);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _coins.Count; i++)
        {
            _coins[i].SetActive(true);
        }
    }
}