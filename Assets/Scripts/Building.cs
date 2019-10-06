using UnityEngine;
using System.Collections;
using System;

public class Building : MonoBehaviour
{

    [SerializeField] float _maxHealth = 100;
    float _health = 0;
    bool isDestroyed = false;
    public bool IsDestroyed { get { return isDestroyed; } }

    [SerializeField] float _regenDeleayInSeconds = 1f;
    [SerializeField] float _regenAmount = 5f;

    float _timeBetweenRegen;

    [SerializeField] GameObject[] _floors;
    int _activeFloor = 0;
    [SerializeField] GameObject _destroyedFloor;
    [SerializeField] GameObject _baseFloor;

    void Start()
    {
    }

    private void Update()
    {
        if (!isDestroyed)
        {
            if (_timeBetweenRegen >= _regenDeleayInSeconds)
            {
                _timeBetweenRegen = 0;
                RegenLife();
            }
            else
            {
                _timeBetweenRegen += Time.deltaTime;
            }
        }
    }

    private void RegenLife()
    {
        _health = Mathf.Min(_health + _regenAmount, _maxHealth);
        if (_health == _maxHealth)
        {
            AddFloor();
        }
    }

    private void AddFloor()
    {
        if (_activeFloor >= _floors.Length) return;

        _baseFloor.SetActive(false);
        _activeFloor += 1;
        _floors[_activeFloor-1].SetActive(true);
        _health = 0;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        if (_health == 0)
        {
            RemoveFloor();
        }
    }

    private void RemoveFloor()
    {
        if (_activeFloor <= 1)
        {
            _floors[0].SetActive(false);
            _baseFloor.SetActive(false);
            _destroyedFloor.SetActive(true);
            isDestroyed = true;
        }
        else
        {
            _floors[_activeFloor - 1].SetActive(false);
            _activeFloor -= 1;
            _health = _maxHealth;
        }
    }
}
