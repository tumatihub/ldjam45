using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject _enemyPrefab;

    private void Start()
    {
        InvokeRepeating("Spawn", 2f, 2f);
    }

    private void Spawn()
    {
        Instantiate(_enemyPrefab);
    }
}
