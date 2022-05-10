using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using IJunior.TypedScenes;
using TMPro;

public class UpgradeStation : MonoBehaviour, ISceneLoadHandler<List<Upgrade>>
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private Player _player;
    [SerializeField] private PhysicsMovement _physicsMovement;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private GameObject _errorScreen;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AudioResources _audioResources;

    private const string Error = "Error";

    public event UnityAction ActivateUpgradeStation;
    public event UnityAction SellUpgrade;

    public List<Upgrade> Upgrades => _upgrades;
    public bool MedKitIsBuyed { get; private set; } = false;

    public void OnSceneLoaded(List<Upgrade> argument)
    {
        _upgrades = argument;
    }

    public void ActivateUpgradeScreen()
    {
        ActivateUpgradeStation?.Invoke();
        _coinText.text = _wallet.Coins.ToString();
    }

    public bool ContainsUpgrade()
    {
        return _upgrades != null && _upgrades.Count > 1;
    }

    private void Start()
    {
        if (_upgrades.Count >= 1)
        {
            MedKitIsBuyed = true;
        }

        InstantiateRandomUpgrades();
    }

    private void InstantiateRandomUpgrades()
    {       
        if (ContainsUpgrade())
        {
            int requiredNumber = 2;
            int firstIndex = Random.Range(1, _upgrades.Count);
            int secondIndex = Random.Range(1, _upgrades.Count);

            if (_upgrades.Count == requiredNumber)
            {
                AddItem(_upgrades[firstIndex]);
            }
            else
            {
                while (firstIndex == secondIndex)
                {
                    secondIndex = Random.Range(1, _upgrades.Count);
                }

                AddItem(_upgrades[firstIndex]);
                AddItem(_upgrades[secondIndex]);
            }
        }
    }

    private void AddItem(Upgrade upgrade)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        upgrade.Init(_player, _physicsMovement);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(upgrade);
    }

    private void OnSellButtonClick(Upgrade upgrade, UpgradeView view)
    {
        if (TrySellUpgrade(upgrade, view) == false)
        {
            _errorScreen.SetActive(true);
            _audioResources.PlaySound(Error);
        }
    }

    private bool TrySellUpgrade(Upgrade upgrade, UpgradeView view)
    {
        if (_wallet.TryDecreaseMoney(upgrade.Price))
        {
            _wallet.DecreaseMoney(upgrade.Price);
            _player.BuyUpgrade(upgrade);
            upgrade.Buy();
            view.SellButtonClick -= OnSellButtonClick;
            SellUpgrade?.Invoke();
            return true;
        }

        return false;
    }
}