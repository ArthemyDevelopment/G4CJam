using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SaturationController : MonoBehaviour
{
    [FoldoutGroup("Saturation Level"), SerializeField]private float ValueForEachLevel;
    [FoldoutGroup("Saturation Level"), SerializeField]private float StartingSaturationValue;
    [FoldoutGroup("Saturation Level"), SerializeField] private float FinalSaturationLevel = 0;
    
    private PostProcessVolume CameraVolume;
    private ColorGrading ActCameraGrading;


    private void Awake()
    {
        CameraVolume = GetComponent<PostProcessVolume>();
        CameraVolume.profile.TryGetSettings(out ActCameraGrading);
    }

    public void AddSaturation()
    {
        ActCameraGrading.saturation.value += ValueForEachLevel;
        if (ActCameraGrading.saturation.value > FinalSaturationLevel)
            ActCameraGrading.saturation.value = FinalSaturationLevel;

    }

    public void ResetLevel()
    {
        ActCameraGrading.saturation.value = StartingSaturationValue;
    }
}
