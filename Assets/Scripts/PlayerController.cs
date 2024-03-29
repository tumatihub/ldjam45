﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 100f;

    Rigidbody _rb;

    Vector3 _movement;

    Vector3 _mousePos;

    [SerializeField] Camera _cam;

    [SerializeField] Transform _bulletPoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed = 300f;
    [SerializeField] float _bulletDamage = 5f;
    private float _bulletLifeTime = 5f;
    [SerializeField] float _bulletDelay = .5f;
    float _bulletTimer;
    [SerializeField] float _impulse = 300f;
    float _dashingCount = 0;
    [SerializeField] float _dashingCooldown = 1f;

    [SerializeField] ParticleSystem _dashParticles;
    AudioSource _audioSource;
    [SerializeField] AudioClip _shootSFX;
    [SerializeField] AudioClip _dashSFX;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        if (_dashingCount >= 0)
        {
            _dashingCount -= Time.deltaTime;
        }

        if (_bulletTimer > 0)
        {
            _bulletTimer -= Time.deltaTime;
        }

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");
        

        Ray _cameraRay = _cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float _rayLength;

        if (groundPlane.Raycast(_cameraRay, out _rayLength))
        {
            Vector3 _pointToLook = _cameraRay.GetPoint(_rayLength);
            transform.LookAt(new Vector3(_pointToLook.x, transform.position.y, _pointToLook.z)); 
            Debug.DrawLine(_cameraRay.origin, _pointToLook, Color.blue);
        }

        if (Input.GetMouseButton(0) && _bulletTimer <= 0)
        {
            _audioSource.PlayOneShot(_shootSFX);
            _bulletTimer = _bulletDelay;
            var _bullet = Instantiate(_bulletPrefab, _bulletPoint.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;
            _bullet.GetComponent<Bullet>().Damage = _bulletDamage;
            Destroy(_bullet, _bulletLifeTime);
        }   
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed * Time.fixedDeltaTime;

        if (Input.GetMouseButtonDown(1) && _dashingCount <= 0)
        {
            _audioSource.PlayOneShot(_dashSFX);
            _rb.AddForce(transform.forward * _impulse, ForceMode.Impulse);
            _dashingCount = _dashingCooldown;
            _dashParticles.Play();
        }
        
    }
}
