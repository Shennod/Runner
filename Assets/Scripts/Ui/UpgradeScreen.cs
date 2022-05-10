using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeScreen;
    [SerializeField] private UpgradeStation _upgradeStation;
    [SerializeField] private PhysicsMovement _physicsMovement;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";

    public void CloseUpgradeScreen()
    {
        Time.timeScale = 1;
        _upgradeScreen.SetActive(false);
        _physicsMovement.TryToMove(true);
        _pauseButton.interactable = true;
        _audioResources.PlaySound(Button);
    }

    private void OnEnable()
    {
        _upgradeStation.ActivateUpgradeStation += OpenUpgradeScreen;
        _upgradeStation.SellUpgrade += CloseUpgradeScreen;
    }

    private void OnDisable()
    {
        _upgradeStation.ActivateUpgradeStation -= OpenUpgradeScreen;
        _upgradeStation.SellUpgrade -= CloseUpgradeScreen;
    }

    private void OpenUpgradeScreen()
    {
        _upgradeScreen.SetActive(true);
        _pauseButton.interactable = false;
        Time.timeScale = 0;
    }
}