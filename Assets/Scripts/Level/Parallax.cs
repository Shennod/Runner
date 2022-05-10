using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PhysicsMovement _player;

    private RawImage _image;

    private float _imagePositionX;

    private void OnEnable()
    {
        _player.MoveParalax += Move;
    }

    private void OnDisable()
    {
        _player.MoveParalax -= Move;
    }

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void Move()
    {
        _imagePositionX += _speed * Time.deltaTime;

        if (_imagePositionX > 1)
        {
            _imagePositionX = 0;
        }

        _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
    }
}