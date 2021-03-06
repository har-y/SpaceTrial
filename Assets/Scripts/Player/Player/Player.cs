﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private AudioController _audioController;

    [Header("Player Prefabs")]
    [SerializeField] private GameObject _ship;
    [SerializeField] private GameObject _laserPosition;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _explosionParticle;

    [Header("Player Configuration")]
    [SerializeField] private float _playerHealth = 200f;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _explosionDelay = 0.75f;

    private Text _playerHealthText;

    private GameObject _root;

    private Vector2 _playerInput;
    private Vector2 _position;
    private Vector2 _min;
    private Vector2 _max;

    private float _horizontal;
    private float _vertical;
    private float _marginX;
    private float _marginY;
    private float _nextShootInterval;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthText = GameObject.FindGameObjectWithTag("health text").GetComponent<Text>();
        _root = GameObject.FindGameObjectWithTag("root");

        _playerSpeed = 10f;
        _shootInterval = 0.3f;

        PlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    private void PlayerMovement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _playerInput = new Vector2(_horizontal, _vertical).normalized;

        PlayerMovementLimits(_playerInput);
    }

    private void LaserInstantiate()
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        laser.transform.position = _laserPosition.transform.position;
        laser.transform.parent = _root.transform;

        _audioController.PlaySound(_audioController._playerShootSFX);
    }

    private void PlayerShoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (Time.time > _nextShootInterval)
            {
                _nextShootInterval = Time.time + _shootInterval;

                LaserInstantiate();
            }
        }
    }

    private void PlayerMovementLimits(Vector2 direction)
    {
        _min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        _max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        _marginX = _ship.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        _marginY = _ship.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy bullet")
        {
            EnemyBullet enemyBullet = collision.gameObject.GetComponent<EnemyBullet>();

            if (!enemyBullet)
            {
                return;
            }

            enemyBullet.BulletDestroy();

            PlayerDestroy(enemyBullet);
        }

        if (collision.gameObject.tag == "enemy")
        {
            PlayerDestroy();
        }
    }

    private void PlayerDestroy()
    {
        _playerHealth = 0;

        PlayerHealth();

        if (_playerHealth <= 0)
        {
            Destroy(gameObject);

            GameObject playerExplosion = Instantiate(_explosionParticle, transform.position, Quaternion.identity);
            playerExplosion.transform.parent = _root.transform;

            _audioController.PlaySound(_audioController._playerExplosionSFX);

            FindObjectOfType<GameController>().LoadOver();
        }
    }

    private void PlayerDestroy(EnemyBullet enemyBullet)
    {
        _playerHealth -= enemyBullet.GetBulletDamage();

        PlayerHealth();

        if (_playerHealth <= 0)
        {
            Destroy(gameObject);

            GameObject playerExplosion = Instantiate(_explosionParticle, transform.position, Quaternion.identity);
            playerExplosion.transform.parent = _root.transform;

            _audioController.PlaySound(_audioController._playerExplosionSFX);

            FindObjectOfType<GameController>().LoadOver();
        }
    }

    private void PlayerHealth()
    {
        _playerHealthText.text = _playerHealth.ToString();
    }
}
