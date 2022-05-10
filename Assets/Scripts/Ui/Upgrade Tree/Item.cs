using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private List<Item> _connectedItems;
    [SerializeField] private Upgrade _template;
    [SerializeField] private ItemView _itemView;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";
    private Button _button;
    private Image _image;

    public Sprite Icon => _icon;
    public string Label => _label;
    public string Description => _description;
    public int Price => _price;
    public bool IsBuyed => _isBuyed;
    public Upgrade Template => _template;

    public bool CanBuy()
    {
        if (_connectedItems.Count > 0)
        {
            for (int i = 0; i < _connectedItems.Count; i++)
            {
                if (_connectedItems[i].IsBuyed)
                {
                    return IsBuyed == false;                 
                }
                else
                {
                    continue;
                }
            }

            return false;
        }
        else
        {
            return IsBuyed == false;
        }
    }

    public void ChangeBuyedFlag()
    {
        _isBuyed = true;
        ChangeImageColor();
    }

    private void ChangeImageColor()
    {
        if (_isBuyed)
        {
            Color normalColor = Color.cyan;
            _image.color = normalColor;
        }
    }

    private void OnButtonClick()
    {
        _audioResources.PlaySound(Button);
        _itemView.gameObject.SetActive(true);
        _itemView.Render(this);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        ChangeImageColor();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}