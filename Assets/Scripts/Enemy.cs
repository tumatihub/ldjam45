using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _maxHealth = 10;
    float _health;

    [SerializeField] Transform _target;
    [SerializeField] float _damage = 10f;
    [SerializeField] float _distanceToAttack = 2f;
    [SerializeField] float _searchRange = 10f;

    enum State { SEARCHING, HITING, WALKING}

    State _state = State.SEARCHING;

    NavMeshAgent _navAgent;
    NavMeshObstacle _navObstacle;

    Animator _anim;

    void Start()
    {
        _health = _maxHealth;
        _navAgent = GetComponent<NavMeshAgent>();
        _navObstacle = GetComponent<NavMeshObstacle>();
        _navObstacle.enabled = false;

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
                break;
            case State.WALKING:
                if (GetIsInRange())
                {
                    _navAgent.isStopped = true;
                    _navAgent.enabled = false;
                    _navObstacle.enabled = true;
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
        var _objs = Physics.OverlapSphere(transform.position, _searchRange);
        List<Transform> _buildingsToChoose = new List<Transform>();
        
        Building _chosen = null;
        foreach (var _obj in _objs)
        {
            var _building = _obj.GetComponent<Building>();
            if (_building != null && !_building.IsDestroyed)
            {
                _buildingsToChoose.Add(_obj.transform);
            }
        }

        if (_buildingsToChoose.Count == 0)
        {
            _searchRange *= 2;
            _state = State.SEARCHING;
            return;
        }

        var _index = UnityEngine.Random.Range(0, _buildingsToChoose.Count);
        _chosen = _buildingsToChoose[_index].GetComponent<Building>();
        _target = _chosen.transform;
        _state = State.WALKING;
        _navAgent.destination = _target.transform.position;
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
            _navObstacle.enabled = false;
            _navAgent.enabled = true;
            _state = State.SEARCHING;
            _anim.SetBool("hitting", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _searchRange);
    }
}
