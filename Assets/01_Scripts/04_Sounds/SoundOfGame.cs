using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundofGame2 : MonoBehaviour
{
    public GameObject capsule;
    AudioSource _volume;
    public AudioClip sound;
    private bool _hasEntered;

    void Start()
    {
        _volume = capsule.GetComponent<AudioSource>();
        //_volume.volume = 0f;

    }
   

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "SoundCube" && !_hasEntered)
        {
            _hasEntered = true;
            AudioSource.PlayClipAtPoint(sound, transform.position);
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Cube2" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Sphere" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Cylinder" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Cylinder2" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }

    }
}
