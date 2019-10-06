using System.Collections;
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

    void Start()
    {
        _rb = GetComponent<Rigidbody>();     
    }
    
    
    void Update()
    {
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

        if (Input.GetMouseButtonDown(0))
        {
            var _bullet = Instantiate(_bulletPrefab, _bulletPoint.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;
            _bullet.GetComponent<Bullet>().Damage = _bulletDamage;
            Destroy(_bullet, _bulletLifeTime);
        }   
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed * Time.fixedDeltaTime;
        
    }
}
