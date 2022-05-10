using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Wallet))]
public class UpgradeTree : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private ItemView _itemView;
    [SerializeField] private GameObject _errorScreen;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private AudioResources _audioResources;
    [SerializeField] private TMP_Text _coinText;

    private Wallet _wallet;
    private List<Upgrade> _buyedUpgrades = new List<Upgrade>();
    private const string Button = "Button";
    private const string Error = "Error";
    private const string MainSave = "MainSave";
    private bool[] _isBuyedFlags;
    private Item _selectedItem;

    public List<Upgrade> GetBuyedUpgrades()
    {
        _buyedUpgrades.Clear();

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].IsBuyed)
            {
                _buyedUpgrades.Add(_items[i].Template);
            }
        }

        return _buyedUpgrades;
    }

    private void Awake()
    {
        _isBuyedFlags = new bool[_items.Count];
        _wallet = GetComponent<Wallet>();
    }

    private void Start()
    {
        Load();

        for (int i = 0; i < _items.Count; i++)
        {
            bool state = _isBuyedFlags[i];

            if (state == true)
            {
                _items[i].ChangeBuyedFlag();
            }
        }
    }

    private void ChangeCoinText()
    {
        _coinText.text = _wallet.Coins.ToString();
    }

    private void OnEnable()
    {
        _itemView.SellButtonClick += OnSellButtonClick;
        _itemView.SelectedItem += ChangeSelectedItem;
    }

    private void OnDisable()
    {
        _itemView.SellButtonClick -= OnSellButtonClick;
        _itemView.SelectedItem -= ChangeSelectedItem;
    }

    private void ChangeSelectedItem(Item item)
    {
        _selectedItem = item;
        ChangeCoinText();
    }

    private void OnSellButtonClick()
    {
        if (TrySellItem() == false)
        {
            _errorScreen.SetActive(true);
            _audioResources.PlaySound(Error);
        }
    }

    private bool TrySellItem()
    {
        if (_wallet.TryDecreaseMoney(_selectedItem.Price))
        {
            if (_selectedItem.CanBuy())
            {
                _audioResources.PlaySound(Button);
                _buttonBuy.interactable = false;
                _selectedItem.ChangeBuyedFlag();
                _wallet.DecreaseMoney(_selectedItem.Price);
                ChangeCoinText();
                Save();
            }

            return true;
        }

        return false;
    }

    private void Save()
    {
        SaveSystem.Save(MainSave, GetSaveSnapshot());
    }

    private void Load()
    {
        var data = SaveSystem.Load<SaveData.PlayerData>(MainSave);
        _isBuyedFlags = data.IsBuyed;
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _isBuyedFlags[i] = _items[i].IsBuyed;
        }

        var data = new SaveData.PlayerData()
        {
            IsBuyed = _isBuyedFlags,
        };

        return data;
    }
}