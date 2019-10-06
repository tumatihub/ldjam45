using UnityEngine;
using System.Collections;
using System;

public class Building : MonoBehaviour
{

    [SerializeField] float _maxHealth = 100;
    float _health;
    bool isDestroyed = false;
    public bool IsDestroyed { get { return isDestroyed; } }

    void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        if (_health == 0)
        {
            DestroyBuildingPart();
        }
    }

    private void DestroyBuildingPart()
    {
        gameObject.SetActive(false);
        isDestroyed = true;
    }
}
