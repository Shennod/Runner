using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashCooldown;
    [SerializeField] private CountdownTimerToStart timerToStart;
    [SerializeField] private AudioResources _audioResources;
    [SerializeField] private ParticleSystem _dashParticle;

    private float _dashTime = 0.1f;
    private float _dashSpeed = 10f;
    private float _lastDash = -1f;
    private bool _isDashing;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _currentVelocity;
    private PlayerInput _playerInput;
    private const string DashSound = "Dash";

    public event UnityAction Dashing;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Dash.performed += ctx =>
        {
            if (ctx.interaction is MultiTapInteraction)
            {
                TryToDash();
            }
        };

        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timerToStart.Run += EnablePlayerInput;
    }

    private void OnDisable()
    {
        timerToStart.Run -= EnablePlayerInput;
        _playerInput.Disable();
    }

    private void EnablePlayerInput()
    {
        _playerInput.Enable();
    }

    private void TryToDash()
    {       
        if (Time.time >= _lastDash + _dashCooldown)
        {
            PrepareToDash();
            StartCoroutine(StopDashing(_dashTime));
        }      
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            _rigidbody2D.velocity += Vector2.right * (_dashSpeed + Time.deltaTime);           
        }
    }

    private void PrepareToDash()
    {
        _currentVelocity = _rigidbody2D.velocity;
        _isDashing = true;
        _lastDash = Time.time;
        _dashParticle.Play();
        Dashing?.Invoke();
        _audioResources.PlaySound(DashSound);
    }

    private IEnumerator StopDashing(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);
        _rigidbody2D.velocity = _currentVelocity;
        _isDashing = false;
        _dashParticle.Stop();
    }
}