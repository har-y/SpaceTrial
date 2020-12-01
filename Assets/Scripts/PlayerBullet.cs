using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private Vector2 _position;
    private Vector2 _max;

    // Start is called before the first frame update
    void Start()
    {
        _bulletSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
        MovementLimits();
    }

    private void BulletMove()
    {
        _position = transform.position;
        _position = new Vector2(_position.x, _position.y + _bulletSpeed * Time.deltaTime);

        transform.position = _position;
    }

    private void MovementLimits()
    {
        _max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        if (transform.position.y > _max.y)
        {
            Destroy(gameObject);
        }
    }
}
