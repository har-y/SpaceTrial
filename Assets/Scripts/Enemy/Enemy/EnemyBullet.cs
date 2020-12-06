using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private Vector2 _position;
    private Vector2 _min;

    // Start is called before the first frame update
    void Start()
    {
        _bulletSpeed = 5f;
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
        _position.y -= _bulletSpeed * Time.deltaTime;

        transform.position = _position;
    }

    private void BulletMovementLimits()
    {
        _min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));

        if (transform.position.y < _min.y)
        {
            Destroy(gameObject);
        }
    }
}
