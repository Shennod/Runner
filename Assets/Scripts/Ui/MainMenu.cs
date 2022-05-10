using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _upgradeTreeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private CanvasGroup _menuScreen;
    [SerializeField] private CanvasGroup _upgradeTreeScreen;
    [SerializeField] private UpgradeTree _upgradeTree;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    private void Start()
    {
        _menuScreen.alpha = 1;
        _upgradeTreeScreen.alpha = 0;
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnButtonClickPlay);
        _upgradeTreeButton.onClick.AddListener(OnButtonClickUpgradeTree);
        _quitButton.onClick.AddListener(OnButtonClickQuit);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnButtonClickPlay);
        _upgradeTreeButton.onClick.RemoveListener(OnButtonClickUpgradeTree);
        _quitButton.onClick.RemoveListener(OnButtonClickQuit);
    }

    private void OnButtonClickPlay()
    {
        _audioResources.PlaySound(Button);
        List<Upgrade> upgrades = _upgradeTree.GetBuyedUpgrades();
        Game.Load(upgrades);
    }

    private void OnButtonClickUpgradeTree()
    {
        _audioResources.PlaySound(Button);
        _menuScreen.gameObject.SetActive(false);
        _upgradeTreeScreen.alpha = 1;
        _upgradeTreeScreen.gameObject.SetActive(true);
    }

    private void OnButtonClickQuit()
    {
        _audioResources.PlaySound(Button);
        Application.Quit();
    }
}