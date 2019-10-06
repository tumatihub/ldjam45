using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform _targetToFollow;

    void Update()
    {
        transform.position = new Vector3(_targetToFollow.transform.position.x, transform.position.y, _targetToFollow.transform.position.z);
    }
}
