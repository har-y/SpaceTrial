﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemyHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBullet playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
        playerBullet.BulletDestroy();

        EnemyDestroy(playerBullet);
    }

    private void EnemyDestroy(PlayerBullet playerBullet)
    {
        _enemyHealth -= playerBullet.GetBulletDamage();

        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
