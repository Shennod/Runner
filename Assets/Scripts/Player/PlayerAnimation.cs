using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PhysicsMovement))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Dash))]
public class PlayerAnimation : MonoBehaviour
{
    private const string Idle = "Idle";
    private const string Move = "Move";
    private const string Hurt = "Hurt";
    private const string InAir = "InAir";
    private const string Dash = "Dash";

    private Animator _animator;

    private PhysicsMovement _physicsMovement;
    private Dash _dash;
    private Player _player;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _physicsMovement.MovePlayer += Run;
        _physicsMovement.PlayerIsJumping += IsJumping;
        _player.Hurt += TakeDamage;
        _dash.Dashing += Dashing;
    }

    private void OnDisable()
    {
        _physicsMovement.MovePlayer -= Run;
        _physicsMovement.PlayerIsJumping -= IsJumping;
        _player.Hurt -= TakeDamage;
        _dash.Dashing -= Dashing;
    }

    private void Awake()
    {
        _dash = GetComponent<Dash>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _physicsMovement = GetComponent<PhysicsMovement>();
        _player = GetComponent<Player>();
        _animator.Play(Idle);
    }

    private void Run(bool state)
    {
        _spriteRenderer.flipX = false;
        _animator.SetBool(Move, state);
    }

    private void IsJumping(bool state)
    {
        _animator.SetBool(InAir, state);
    }

    private void TakeDamage()
    {
        _animator.Play(Hurt);
    }

    private void Dashing()
    {
        _animator.Play(Dash);
    }
}