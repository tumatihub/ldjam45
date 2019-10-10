using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
