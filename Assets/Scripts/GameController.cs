﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Building> _buildings = new List<Building>();
    [SerializeField] List<EnemySpawner> _spawners = new List<EnemySpawner>();
    [SerializeField] GameObject _firstBuilding;
    [SerializeField] GameObject _firstSpawner;

    [SerializeField] float _timeToNextSpawner = 20f;
    float _timer = 0;

    [SerializeField] TMP_Text _multiplierTextUI;
    [SerializeField] TMP_Text _scoreTextUI;
    int _score = 0;
    [SerializeField] int _enemyDeathBaseScore = 5;
    int _scoreMultiplier = 1;

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
        if (_spawners.Count == 0) return;
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
        if (_buildings.Count == 0) return;
        var _index = Random.Range(0, _spawners.Count);
        var _building = _buildings[_index];
        _building.gameObject.SetActive(true);
        _buildings.RemoveAt(_index);
    }

    void UpdateScore()
    {
        _scoreTextUI.text = _score.ToString();
    }

    void UpdateMultiplier()
    {
        _multiplierTextUI.text = "x" + _scoreMultiplier.ToString();
    }

    public void IncreaseMultiplier()
    {
        _scoreMultiplier *= 2;
        UpdateMultiplier();
    }

    public void DecreaseMultiplier()
    {
        _scoreMultiplier = Mathf.Max(_scoreMultiplier/2, 1);
        UpdateMultiplier();
    }

    public void ScoreEnemyDeath()
    {
        _score += _enemyDeathBaseScore * _scoreMultiplier;
        UpdateScore();
    }


}
