using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;

    private Vector3 _playerInput;

    private float _horizontal;
    private float _vertical;

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
        _horizontal = Input.GetAxis("Horizontal") *_playerSpeed * Time.deltaTime;
        _vertical = Input.GetAxis("Vertical") * _playerSpeed * Time.deltaTime;

        _playerInput = new Vector3(_horizontal, _vertical, 0f);

        transform.position += _playerInput;
    }
}
