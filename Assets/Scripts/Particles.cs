using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private AudioClip explosion;
    void Start()
    {
        SoundManager.Instance.EjecutarAudio(explosion);
        Destroy(gameObject, 1);
    }


}
