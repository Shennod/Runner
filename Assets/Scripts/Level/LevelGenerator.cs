using UnityEngine;

public class LevelGenerator : ObjectPool
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private UpgradeStation _upgradeStation;

    private int _levelsBeforeSpawnMedKit = 5;
    private int _levelCount;
    private Vector3 _offset = new Vector3(26, 0, 0);
    private Vector3 _lastEndPosition;

    private void Start()
    {
        _lastEndPosition = _offset;
        Initialize();
    }

    private void OnEnable()
    {
        _player.LevelChanged += SpawnLevelPart;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= SpawnLevelPart;
    }

    private void TryToSpawnMedKit(MedKitActivator medKit)
    {      
        if (_upgradeStation.MedKitIsBuyed)
        {
            _levelCount++;

            if (_levelCount >= _levelsBeforeSpawnMedKit)
            {
                _levelCount = 0;
                medKit.ActivateStation();
            }
            else
            {
                medKit.DeactivateStation();
            }
        }
    }

    private void SpawnLevelPart()
    {
        if (TryGetRandomObject(out GameObject levelPart))
        {
            levelPart.transform.position = _lastEndPosition + _lastEndPosition;
            _lastEndPosition += _offset;

            levelPart.gameObject.SetActive(true);
            MedKitActivator medKit = levelPart.GetComponent<MedKitActivator>();

            TryToSpawnMedKit(medKit);

            DisabelObjectAbroadScreen(_camera);
        }
    }
}