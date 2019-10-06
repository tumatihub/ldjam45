using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _spawnDelayInSeconds = 3f;

    private void Start()
    {
        InvokeRepeating("Spawn", 2f, _spawnDelayInSeconds);
    }

    private void Spawn()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
