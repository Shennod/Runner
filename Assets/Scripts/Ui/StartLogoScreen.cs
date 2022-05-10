using IJunior.TypedScenes;
using UnityEngine.UI;
using UnityEngine;

public class StartLogoScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    private void OnEnable()
    {
        _buttonStart.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buttonStart.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _audioResources.PlaySound(Button);
        Menu.Load();
    }
}