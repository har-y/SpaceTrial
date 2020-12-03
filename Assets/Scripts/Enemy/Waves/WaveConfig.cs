using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration", fileName = "Wave")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;

    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private float _spawnFactor = 0.3f;
    [SerializeField] private float _enemySpeed = 2f;

    [SerializeField] private int _enemyNumber = 5;

    public GameObject GetEnemyPrefab()
    {
        return _enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waveWaypoints = new List<Transform>();

        foreach (Transform item in _pathPrefab.transform)
        {
            waveWaypoints.Add(item);
        }


        return waveWaypoints;
    }

    public float GetSpawnInterval()
    {
        return _spawnInterval;
    }

    public float GetSpawnFactor()
    {
        return _spawnFactor;
    }

    public float GetEnemySpeed()
    {
        return _enemySpeed;
    }

    public int GetEnemyNumber()
    {
        return _enemyNumber;
    }
}
