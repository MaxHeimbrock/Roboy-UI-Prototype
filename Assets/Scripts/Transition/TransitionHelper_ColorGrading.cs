using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the color grading effect to be animated by an animator.
 */
public class TransitionHelper_ColorGrading : MonoBehaviour
{
    private ColorGrading colorGrading;

    [SerializeField]
    private float temperature = 0.0f;
    [SerializeField]
    private float contrast = 0.0f;

    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    void LateUpdate()
    {
        colorGrading.temperature.value = temperature;
        colorGrading.contrast.value = contrast;
    }
}
