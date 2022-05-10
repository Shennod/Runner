using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelView;
    [SerializeField] private TMP_Text _descriptionView;
    [SerializeField] private Image _imageView;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private TMP_Text _price;

    private Item _item;

    public event UnityAction SellButtonClick;
    public event UnityAction<Item> SelectedItem;

    public void Render(Item item)
    {
        SelectedItem?.Invoke(item);

        _item = item;
        _price.text = item.Price.ToString();
        _labelView.text = item.Label;
        _descriptionView.text = item.Description;
        _imageView.sprite = item.Icon;

        if (_item.CanBuy())
        {
            _buttonBuy.interactable = true;
        }
        else
        {
            _buttonBuy.interactable = false;
        }      
    }

    private void OnEnable()
    {
        _buttonBuy.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buttonBuy.onClick.RemoveListener(OnButtonClick);
    } 

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke();
    }
}