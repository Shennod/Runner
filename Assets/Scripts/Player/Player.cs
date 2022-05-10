using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Dash))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private ParticleSystem _electroProtectParticle;
    [SerializeField] private AudioResources _audioResources;

    private int _health;
    private int _armor;
    private float _timeInvincible = 0.3f;
    private bool _isInvincible = false;
    private bool _isElectroProtected = false;
    private Dash _dash;

    private const string GameOver = "GameOver";
    private const string Hit = "Hit";
    private const string Activate = "Activate";
    private const string ElectroTrapDamage = "ElectroTrapDamage";
    private const string Armor = "Armor";
    private const string HealthUp = "HealthUp";
    private const string FullHealth = "FullHealth";
    private const string ElectroProtect = "ElectroProtect";

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> ArmorChanged;
    public event UnityAction<Upgrade> UpgradeChanged;
    public event UnityAction Hurt;
    public event UnityAction ElectroHurt;
    public event UnityAction Died;
    public event UnityAction LevelChanged;
    public event UnityAction<MedKitStation> ActivateMedKitStation;

    public void ApplyDamage(int damage, bool isElectroTrap)
    {
        if (_isInvincible == false)
        {
            if (isElectroTrap && _isElectroProtected == false)
            {
                ElectroHurt?.Invoke();
                TryDecreaseHealth(damage);
                _audioResources.PlaySound(ElectroTrapDamage);
            }

            if (isElectroTrap == false)
            {
                Hurt?.Invoke();
                TryDecreaseHealth(damage);
                _audioResources.PlaySound(Hit);
            }
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    public void ActivateMedKitScreen(MedKitStation medKitStation)
    {
        ActivateMedKitStation?.Invoke(medKitStation);
        _audioResources.PlaySound(Activate);
    }

    public void OnLevelChange()
    {
        LevelChanged?.Invoke();
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        upgrade.Activate();
        UpgradeChanged?.Invoke(upgrade);
    }

    public bool NeedHeal()
    {
        return _health < _maxHealth;
    }

    public void BuyMedKit()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health);
        _audioResources.PlaySound(FullHealth);
    }

    public void SetArmor(int value)
    {
        _armor = value;
        ArmorChanged?.Invoke(_armor);
        _audioResources.PlaySound(Armor);
    }

    public void SetHealth(int value)
    {
        _maxHealth += value;
        _health = _maxHealth;
        HealthChanged?.Invoke(_health);
        _audioResources.PlaySound(HealthUp);
    }

    public void SetElectroProtect()
    {
        _isElectroProtected = true;
        _electroProtectParticle.Play();
        _audioResources.PlaySound(ElectroProtect);
    }

    private void Awake()
    {
        _dash = GetComponent<Dash>();
    }

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health);
    }

    private void OnEnable()
    {
        _dash.Dashing += ActivateInvincible;
    }

    private void OnDisable()
    {
        _dash.Dashing -= ActivateInvincible;
    }

    private void ActivateInvincible()
    {
        _isInvincible = true;
        StartCoroutine(Invincible(_timeInvincible));
    }

    private void TryDecreaseHealth(int damage)
    {
        if (_armor > 0)
        {
            _armor -= damage;
            ArmorChanged?.Invoke(_armor);
        }
        else
        {
            _health -= damage;
            HealthChanged?.Invoke(_health);
        }
        ActivateInvincible();
    }

    private void Die()
    {
        Died?.Invoke();
        _audioResources.PlaySound(GameOver);
    }

    private IEnumerator Invincible(float timeInvincible)
    {
        while (_isInvincible)
        {
            yield return new WaitForSeconds(timeInvincible);
            _isInvincible = false;
        }
    }
}