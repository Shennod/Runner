using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Dash))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CountdownTimerToStart timerToStart;
    [SerializeField] private AudioResources _audioResources;
    [SerializeField] private ParticleSystem _speedUpParticle;

    private float _minGroundNormalY = -1f;
    private float _jumpTime = 1f;
    private float _jumpForce = 8f;
    private float _speed = 5f;
    private float _gravityModifier = 2f;
    private float _jumpTimeCounter;
    private bool _isJumping;
    private bool _canJump;
    private Vector2 _jumpVelocity;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidbody2D;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private PlayerInput _playerInput;
    private Dash _dash;

    private const string Jump = "Jump";
    private const string SpeedUp = "SpeedUp";
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    public event UnityAction MoveParalax;
    public event UnityAction<bool> MovePlayer;
    public event UnityAction<bool> PlayerIsJumping;

    public void TryToMove(bool state)
    {
        if (state == true)
        {
            _targetVelocity = new Vector2(1, 0);
        }
        else
        {
            _targetVelocity = new Vector2(0, 0);
        }

        MovePlayer?.Invoke(state);
    }

    public void SetMoveSpeed(int value)
    {
        _speed = value;
        _speedUpParticle.Play();
        _audioResources.PlaySound(SpeedUp);
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dash = GetComponent<Dash>();
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += ctx => JumpStarted();
        _playerInput.Player.Jump.canceled += ctx => JumpEnd();
    }

    private void OnEnable()
    {
        timerToStart.Run += EnablePlayerInput;
        _dash.Dashing += JumpEnd;
    }

    private void OnDisable()
    {
        timerToStart.Run += EnablePlayerInput;
        _playerInput.Disable();
        _dash.Dashing -= JumpEnd;
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
        TryToMove(false);
    }

    private void EnablePlayerInput()
    {
        _playerInput.Enable();
        TryToMove(true);
    }

    private void JumpStarted()
    {
        if (_canJump)
        {
            _canJump = false;
            _jumpVelocity.y = _jumpForce;
            _isJumping = true;
            PlayerIsJumping?.Invoke(_isJumping);
            _jumpTimeCounter = _jumpTime;
            _audioResources.PlaySound(Jump);
        }
    }

    private void JumpEnd()
    {
        _isJumping = false;
        PlayerIsJumping?.Invoke(_isJumping);
    }

    private void Update()
    {
        if (_jumpVelocity.normalized.magnitude > 0.1f)
        {
            MoveParalax?.Invoke();
        }

        if (_isJumping)
        {
            if (_jumpTimeCounter > 0)
            {
                _jumpVelocity.y = _jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
                PlayerIsJumping?.Invoke(_isJumping);
            }
        }
    }

    private void FixedUpdate()
    {
        _jumpVelocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _jumpVelocity.x = _targetVelocity.x * _speed;

        Vector2 deltaPosition = _jumpVelocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movements(move, false);

        move = Vector2.up * deltaPosition.y;

        Movements(move, true);
    }

    private void Movements(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rigidbody2D.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _canJump = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                else
                {
                    _isJumping = false;
                    PlayerIsJumping?.Invoke(_isJumping);
                }

                float projection = Vector2.Dot(_jumpVelocity, currentNormal);
                if (projection < 0)
                {
                    _jumpVelocity = _jumpVelocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody2D.position = _rigidbody2D.position + move.normalized * distance;
    }
}