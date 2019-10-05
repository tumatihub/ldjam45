using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _maxHealth = 10;
    float _health;

    void Start()
    {
        _health = _maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        var _bullet = other.GetComponent<Bullet>();
        if (_bullet != null)
        {
            _health = Mathf.Max(_health - _bullet.GetComponent<Bullet>().Damage, 0);
            if (_health == 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
