using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class TrapAnimation : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] private string _animationName;
    [SerializeField] private Sprite _firstSprite;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    public event UnityAction Activate;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimation(_cooldown));
    }

    private void OnDisable()
    {
        _spriteRenderer.sprite = _firstSprite;
    }

    public void ActivateCollider()
    {
        _boxCollider.enabled = true;
        Activate?.Invoke();
    }

    public void DeactivateCollider()
    {
        _boxCollider.enabled = false;
    }

    private IEnumerator PlayAnimation(float cooldown)
    {
        while (gameObject.activeSelf == true)
        {
            _animator.SetTrigger(_animationName);
            yield return new WaitForSeconds(cooldown);
        }
    }
}