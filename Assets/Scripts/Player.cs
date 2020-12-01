using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;

    private Vector2 _playerInput;
    private Vector2 _position;
    private Vector2 _min;
    private Vector2 _max;

    private float _horizontal;
    private float _vertical;
    private float _marginX;
    private float _marginY;

    // Start is called before the first frame update
    void Start()
    {
        _playerSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _playerInput = new Vector2(_horizontal, _vertical).normalized;

        MovementLimits(_playerInput);
    }

    private void MovementLimits(Vector2 direction)
    {
        _min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        _max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        _marginX = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        _marginY = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;

        _min.x = _min.x + _marginX;
        _min.y = _min.y + _marginY;
        _max.x = _max.x - _marginX;
        _max.y = _max.y - _marginY;

        _position = transform.position;

        _position += direction * _playerSpeed * Time.deltaTime;
        _position.x = Mathf.Clamp(_position.x, _min.x, _max.x);
        _position.y = Mathf.Clamp(_position.y, _min.y, _max.y);

        transform.position = _position;
    }
}
