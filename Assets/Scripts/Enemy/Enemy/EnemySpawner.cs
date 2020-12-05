using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> _waveConfigs;
    [SerializeField] private GameObject _root;
    [SerializeField] private int _startingWave = 0;

    private WaveConfig _currentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
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
