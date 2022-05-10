public class ArmorBar : Bar
{
    private void OnEnable()
    {
        _player.UpgradeChanged += TryActivateArmorBar;
        _player.ArmorChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.UpgradeChanged -= TryActivateArmorBar;
        _player.ArmorChanged -= OnValueChanged;
    }

    private void TryActivateArmorBar(Upgrade upgrade)
    {
        if (upgrade.NeedExtraBar)
        {
            _container.SetActive(true);
        }
    }
}