using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private AudioController _audioController;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject _laserPosition;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _explosionParticle;

    [Header("Enemy Configuration")]
    [SerializeField] private float _enemyHealth = 100f;
    [SerializeField] private float _minShotTime = 2f;
    [SerializeField] private float _maxShotTime = 6f;
    [SerializeField] private float _explosionDelay = 0.75f;

    private GameObject _root;

    private float _bulletTime;

    // Start is called before the first frame update
    void Start()
    {
        _audioController.GetComponent<AudioController>();
        _root = GameObject.FindGameObjectWithTag("root");
        _bulletTime = Random.Range(_minShotTime, _maxShotTime);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBulletTimeToShoot();
    }

    private void EnemyBulletTimeToShoot()
    {
        _bulletTime -= Time.deltaTime;

        if (_bulletTime <= 0f)
        {
            EnemyShoot();

            _bulletTime = Random.Range(_minShotTime, _maxShotTime);
        }
    }

    private void EnemyShoot()
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        laser.transform.position = _laserPosition.transform.position;
        laser.transform.parent = _root.transform;

        _audioController.PlaySound(_audioController._enemyShootSFX);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player bullet")
        {
            PlayerBullet playerBullet = collision.gameObject.GetComponent<PlayerBullet>();

            if (!playerBullet)
            {
                return;
            }

            playerBullet.BulletDestroy();

            EnemyDestroy(playerBullet);
        }
    }

    private void EnemyDestroy(PlayerBullet playerBullet)
    {
        _enemyHealth -= playerBullet.GetBulletDamage();

        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);

            GameObject enemyExplosion = Instantiate(_explosionParticle, transform.position, Quaternion.identity);
            enemyExplosion.transform.parent = _root.transform;

            _audioController.PlaySound(_audioController._explosionSFX);

            Destroy(enemyExplosion, _explosionDelay);
        }
    }
}
