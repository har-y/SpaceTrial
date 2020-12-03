using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoint;

    [SerializeField] private float _enemySpeed = 2f;

    private int _waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _waypoint[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_waypointIndex <= (_waypoint.Count - 1))
        {
            Vector3 targetPosition = _waypoint[_waypointIndex].transform.position;
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