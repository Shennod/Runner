using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField] private TrapAnimation _trapAnimation;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _trapAnimation.Activate += Play;
    }

    private void OnDisable()
    {
        _trapAnimation.Activate -= Play;
    }

    private void Play()
    {
        _audioSource.Play();
    }
}