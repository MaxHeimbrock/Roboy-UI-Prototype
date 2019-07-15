using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionHelper_ColorGrading : MonoBehaviour
{
    private ColorGrading colorGrading;

    [SerializeField]
    private float temperature = 0.0f;
    [SerializeField]
    private float contrast = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    void LateUpdate()
    {
        colorGrading.temperature.value = temperature;
        colorGrading.contrast.value = contrast;
    }
}
