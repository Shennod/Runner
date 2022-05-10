using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackToMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _upgradeTreeScreen;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _audioResources.PlaySound(Button);
        _menuScreen.SetActive(true);
        _upgradeTreeScreen.SetActive(false);
    }
}