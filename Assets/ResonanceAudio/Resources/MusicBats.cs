
using UnityEngine;
using UnityEngine.Audio;

public class MusicBats : MonoBehaviour
{

    public AudioMixer MusicMixer;
    public float StartVolume;
    public float volumeToAdd;
    float currLevel;

    private void Awake()
    {
        ResetVolume();
    }

    public void AddVolume()
    {
        currLevel += volumeToAdd;
        MusicMixer.SetFloat("Volume", currLevel);
    }

    public void ResetVolume()
    {
       MusicMixer.SetFloat("Volume", StartVolume);
       currLevel = StartVolume;
    }
}
