using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhisperController : MonoBehaviour
{
    public List<AudioSource> WhispersSources;
    public List<Whispers> Whispers;
    public WhisperSubController WhispersSubs;
    public Vector2 RandomRange;
    bool isPlaying;
    bool whisperActive;

    private void OnEnable()
    {
        isPlaying = false;
    }
    
    

    public void StartWhispers()
    {
        isPlaying = true;
        StartCoroutine(WhisperLoop());
    }

    public void TurnOff()
    {
        isPlaying = false;
    }

    IEnumerator WhisperLoop()
    {
        while (isPlaying)
        {
            float temp = Random.Range(RandomRange.x, RandomRange.y);
            yield return ScriptsTools.GetWait(temp);
            if (!whisperActive)
            {
                whisperActive = true;
                int source = Random.Range(0, WhispersSources.Count);
                int whisper = Random.Range(0, Whispers.Count);
                WhispersSources[source].clip = Whispers[whisper].WhisperClip;
                WhispersSources[source].Play();
                WhispersSubs.SetWhispersSub(Whispers[whisper].WhisperClip.length, Whispers[whisper].LocalizationKey);
                yield return ScriptsTools.GetWait(Whispers[whisper].WhisperClip.length);
                whisperActive = false;
            }

        }
    }
    
}

[Serializable]
public struct Whispers
{
    public AudioClip WhisperClip;
    public string LocalizationKey;
}
