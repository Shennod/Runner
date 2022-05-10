using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HelpScreen : MonoBehaviour
{
    [SerializeField] private GameObject _helpScreen;
    [SerializeField] private Button _helpOkButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private AudioResources _audioResources;

    private bool _isFirstRun;
    private const string Button = "Button";
    private const string FirsRun = "FirsRun";

    public event UnityAction CountdownStart;

    private void Start()
    {
        Load();

        if (_isFirstRun)
        {
            _isFirstRun = false;
            _helpScreen.SetActive(true);
            _pauseButton.interactable = false;
            Time.timeScale = 0;
            Save();
        }
        else
        {
            CountdownStart?.Invoke();
        }
    }

    private void OnEnable()
    {
        _helpOkButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _helpOkButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 1;
        CountdownStart?.Invoke();
        _helpScreen.SetActive(false);
        _pauseButton.interactable = true;
        _audioResources.PlaySound(Button);
    }

    private void Load()
    {
        var data = SaveSystem.Load<SaveData.PlayerData>(FirsRun);
        _isFirstRun = data.IsFirstRun;
    }

    private void Save()
    {
        SaveSystem.Save(FirsRun, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            IsFirstRun = _isFirstRun,
        };

        return data;
    }
}