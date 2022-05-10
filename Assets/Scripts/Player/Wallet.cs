using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private AudioResources _audioResources;

    private const string CoinSave = "CoinSave";
    private const string Collect = "Collect";
    private int _collectedCoins = 0;

    public int Coins { get; private set; }

    public void DecreaseMoney(int value)
    {
        Coins -= value;
        ChangeCoinText();
        Save();
    }

    public void DecreaseCollectedMoney(int value)
    {
        _collectedCoins -= value;
        ChangeCoinText();
    }

    public void AddCoin(int value)
    {
        _collectedCoins += value;
        ChangeCoinText();
        _audioResources.PlaySound(Collect);
    }

    public void SaveCollectedCoins()
    {
        Coins += _collectedCoins;
        Save();
    }

    public bool TryDecreaseCollectedMoney(int value)
    {
        return value <= _collectedCoins;
    }

    public bool TryDecreaseMoney(int value)
    {
        return value <= Coins;
    }

    private void Start()
    {
        Load();
        ChangeCoinText();
    }

    private void ChangeCoinText()
    {
        if (_coinText != null)
        {
            _coinText.text = _collectedCoins.ToString();
        }
    }

    private void Save()
    {
        SaveSystem.Save(CoinSave, GetSaveSnapshot());
    }

    private void Load()
    {
        var data = SaveSystem.Load<SaveData.PlayerData>(CoinSave);
        Coins = data.Coins;
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            Coins = Coins,
        };

        return data;
    }
}