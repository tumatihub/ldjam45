using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _maxHealth = 10;
    float _health;

    [SerializeField] Transform _target;
    [SerializeField] float _damage = 10f;
    [SerializeField] float _distanceToAttack = 2f;

    enum State { SEARCHING, HITING, WALKING}

    State _state = State.SEARCHING;

    NavMeshAgent _navAgent;

    Animator _anim;

    void Start()
    {
        _health = _maxHealth;
        _navAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.SEARCHING:
                SearchForTarget();
                break;
            case State.HITING:
                print("Hiting!");
                break;
            case State.WALKING:
                if (GetIsInRange())
                {
                    print("Stopped");
                    _state = State.HITING;
                    transform.LookAt(_target);
                    _anim.SetBool("hitting", true);
                }
                break;
        }
    }

    private bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, _target.position) <= _distanceToAttack;
    }

    private void SearchForTarget()
    {
        var _objs = GameObject.FindObjectsOfType<Building>();

        foreach (var _obj in _objs)
        {
            if (!_obj.IsDestroyed)
            {
                _target = _obj.transform;
                _state = State.WALKING;
                _navAgent.destination = _target.transform.position;
            }
        }
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

    // Animation Event
    public void Hit()
    {
        Building _building = _target.GetComponent<Building>();
        _building.TakeDamage(_damage);
        if (_building.IsDestroyed)
        {
            _state = State.SEARCHING;
            _anim.SetBool("hitting", false);
        }
    }
}
