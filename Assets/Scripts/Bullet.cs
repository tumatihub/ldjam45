using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
