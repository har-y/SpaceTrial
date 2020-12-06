using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner Configuration")]
    [SerializeField] List<WaveConfig> _waveConfigs;
    [SerializeField] private int _startingWave = 0;
    [SerializeField] private bool _loop = false;

    private WaveConfig _currentWave;

    private GameObject _root;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _root = GameObject.FindGameObjectWithTag("root");

        do
        {
            yield return StartCoroutine(SpawnWaves());
        }
        while (_loop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetEnemyNumber(); i++)
        {
            GameObject enemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            enemy.transform.parent = _root.transform;
            enemy.GetComponent<EnemyPath>().SetWaveConfiguration(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetSpawnInterval());
        }
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = _startingWave; i < _waveConfigs.Count; i++)
        {
            _currentWave = _waveConfigs[i];

            yield return StartCoroutine(SpawnEnemies(_currentWave));
        }
    }
}
