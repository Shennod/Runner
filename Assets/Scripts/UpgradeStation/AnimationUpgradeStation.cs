using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UpgradeStation))]
public class AnimationUpgradeStation : MonoBehaviour
{
    [SerializeField] private AudioResources _audioResources;

    private const string Activate = "Activate";
    private const string IsCollected = "IsCollected";
    private const float WaitTimeSeconds = 1f;

    private UpgradeStation _upgradeStation;

    private Animator _animator;

    private void Start()
    {
        _upgradeStation = GetComponent<UpgradeStation>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhysicsMovement player))
        {
            if (_upgradeStation.ContainsUpgrade())
            {
                player.TryToMove(false);
                _animator.SetTrigger(IsCollected);
                StartCoroutine(ActivateUpgradeScreen());
            }
        }
    }

    private IEnumerator ActivateUpgradeScreen()
    {
        yield return new WaitForSeconds(WaitTimeSeconds);
        _upgradeStation.ActivateUpgradeScreen();
        _audioResources.PlaySound(Activate);
    }
}