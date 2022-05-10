using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMainMenuButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private UpgradeStation _upgradeStation;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    private void OnEnable()
    {
        _player.Died += OnDied;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButtonClick);
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _backToMainMenuButton.onClick.RemoveListener(OnBackToMainMenuButtonClick);
    }

    private void OnDied()
    {
        _gameOverScreen.SetActive(true);
        _wallet.SaveCollectedCoins();
        _pauseButton.interactable = false;
        Time.timeScale = 0;
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        List<Upgrade> upgrades = _upgradeStation.Upgrades;
        _gameOverScreen.SetActive(false);
        _pauseButton.interactable = true;
        Game.Load(upgrades);
        _audioResources.PlaySound(Button);
    }

    private void OnBackToMainMenuButtonClick()
    {
        Time.timeScale = 1;
        Menu.Load();
        _audioResources.PlaySound(Button);
    }
}