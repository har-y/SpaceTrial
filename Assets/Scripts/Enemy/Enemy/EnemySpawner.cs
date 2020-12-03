using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> _waveConfigs;

    private WaveConfig _currentWave;

    private int _startingWave = 0;


    // Start is called before the first frame update
    void Start()
    {
        _currentWave = _waveConfigs[_startingWave];
        StartCoroutine(SpawnEnemies(_currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetEnemyNumber(); i++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetSpawnInterval());
        }
    }
}
