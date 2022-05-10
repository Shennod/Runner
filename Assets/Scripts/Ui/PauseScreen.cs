using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _backToMainMenuButton;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButtonClick);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _backToMainMenuButton.onClick.RemoveListener(OnBackToMainMenuButtonClick);
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        _pauseButton.interactable = false;
        _pauseScreen.SetActive(true);
        _audioResources.PlaySound(Button);
    }

    private void OnResumeButtonClick()
    {
        _pauseScreen.SetActive(false);
        _pauseButton.interactable = true;
        Time.timeScale = 1;
        _audioResources.PlaySound(Button);
    }

    private void OnBackToMainMenuButtonClick()
    {
        Menu.Load();
        Time.timeScale = 1;
        _audioResources.PlaySound(Button);
    }
}