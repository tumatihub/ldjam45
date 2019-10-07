using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Building> _buildings = new List<Building>();
    [SerializeField] List<EnemySpawner> _spawners = new List<EnemySpawner>();
    [SerializeField] GameObject _firstBuilding;
    [SerializeField] GameObject _firstSpawner;

    [SerializeField] float _timeToNextSpawner = 20f;
    float _timer = 0;


    // Use this for initialization
    void Start()
    {
        Invoke("StartFirstBuilding", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer >= _timeToNextSpawner)
        {
            ActiveRandomSpawner();
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    void ActiveRandomSpawner()
    {
        var _index = Random.Range(0, _spawners.Count);
        print("Active spawner " + _index);
        var _spawner = _spawners[_index];
        _spawner.gameObject.SetActive(true);
        _spawners.RemoveAt(_index);
    }

    void StartFirstBuilding()
    {
        _firstBuilding.SetActive(true);
        _buildings.Remove(_firstBuilding.GetComponent<Building>());
        StartFirstSpawner();
    }

    void StartFirstSpawner()
    {
        _firstSpawner.SetActive(true);
        _spawners.Remove(_firstSpawner.GetComponent<EnemySpawner>());
    }

    public void ActiveRandomBuilding()
    {
        var _index = Random.Range(0, _spawners.Count);
        var _building = _buildings[_index];
        _building.gameObject.SetActive(true);
        _buildings.RemoveAt(_index);
    }
}
