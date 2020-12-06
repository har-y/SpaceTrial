using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Player Bullet Configuration")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage = 100;

    private Vector2 _position;
    private Vector2 _max;

    // Start is called before the first frame update
    void Start()
    {
        _bulletSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        BulletMovementLimits();
    }

    private void BulletMovement()
    {
        _position = transform.position;
        _position.y += _bulletSpeed * Time.deltaTime;

        transform.position = _position;
    }

    private void BulletMovementLimits()
    {
        _max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        if (transform.position.y > _max.y)
        {
            Destroy(gameObject);
        }
    }

    public int GetBulletDamage()
    {
        return _bulletDamage;
    }

    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
