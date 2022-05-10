using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;
    private const string Shake = "Shake";

    private void OnEnable()
    {
        _player.Hurt += AnimationShake;
    }

    private void OnDisable()
    {
        _player.Hurt -= AnimationShake;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
        
    private void AnimationShake()
    {
        _animator.SetTrigger(Shake);
    }
}