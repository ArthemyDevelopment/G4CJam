using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOfGame : MonoBehaviour
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
        
        

        if (col.gameObject.name == "Mom" && !_hasEntered && Input.GetKeyDown(KeyCode.Q))
        {
            _hasEntered = true;
            AudioSource.PlayClipAtPoint(sound, transform.position);
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Dad" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Bandit" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Nicos" && !_hasEntered)
        {
            _hasEntered = true;
            _volume.volume += 0.2f;
        }
        else
        {
            _hasEntered = false;
        }
        
        if (col.gameObject.name == "Gabe" && !_hasEntered)
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
