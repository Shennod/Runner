using UnityEngine;
using UnityEngine.UI;

public class MedKitScreen : MonoBehaviour
{
    [SerializeField] private GameObject _medKitScreen;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _errorScreen;
    [SerializeField] private PhysicsMovement _physicsMovement;
    [SerializeField] private Player _player;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private AudioResources _audioResources;

    private const string Button = "Button";
    private const string Error = "Error";
    private MedKitStation _medKitStation;

    public void CloseMedKitScreen()
    {
        Time.timeScale = 1;
        _medKitScreen.SetActive(false);
        _physicsMovement.TryToMove(true);
        _pauseButton.interactable = true;
        _audioResources.PlaySound(Button);
    }

    private void OnEnable()
    {
        _player.ActivateMedKitStation += OpenMedKitScreen;
        _sellButton.onClick.AddListener(OnSellButtonClick);
    }

    private void OnDisable()
    {
        _player.ActivateMedKitStation -= OpenMedKitScreen;
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    private void OnSellButtonClick()
    {
        if (_medKitStation.TrySellMedKit())
        {
            CloseMedKitScreen();
        }
        else
        {
            _errorScreen.SetActive(true);
            _audioResources.PlaySound(Error);
        }
    }

    private void OpenMedKitScreen(MedKitStation medKitStation)
    {
        _medKitStation = medKitStation;
        _medKitScreen.SetActive(true);
        _priceText.text = medKitStation.Price.ToString();
        _pauseButton.interactable = false;
        Time.timeScale = 0;
    }
}