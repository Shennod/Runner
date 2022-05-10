using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;

    private Upgrade _upgrade;

    public event UnityAction<Upgrade, UpgradeView> SellButtonClick;

    public void Render(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _label.text = upgrade.Label;
        _price.text = upgrade.Price.ToString();
        _icon.sprite = upgrade.Icon;
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }
    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_upgrade, this);
    }
}