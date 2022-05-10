using UnityEngine;
using UnityEngine.UI;

public class CloseScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonClose;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    private void OnEnable()
    {
        _buttonClose.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buttonClose.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _audioResources.PlaySound(Button);
        gameObject.SetActive(false);
    }
}