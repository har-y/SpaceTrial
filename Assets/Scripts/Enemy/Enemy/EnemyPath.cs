using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private WaveConfig _waveConfig;

    private List<Transform> _waypoints;

    private float _enemySpeed;

    private int _waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _waypoints = _waveConfig.GetWaypoints();
        _enemySpeed = _waveConfig.GetEnemySpeed();

        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_waypointIndex <= (_waypoints.Count - 1))
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].transform.position;
            float enemyMovement = _enemySpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}