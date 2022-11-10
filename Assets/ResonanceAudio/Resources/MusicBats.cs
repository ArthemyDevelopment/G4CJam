
using UnityEngine;
using UnityEngine.Audio;

public class MusicBats : MonoBehaviour
{

    public AudioSource Music;
    public float volumeToAdd;
    float currLevel;

    private void Awake()
    {
        ResetVolume();
    }

    public void AddVolume()
    {
        currLevel += volumeToAdd;
        Music.volume = currLevel;
    }

    public void ResetVolume()
    { 
       Music.volume = 0;
       currLevel = 0;
    }
}
