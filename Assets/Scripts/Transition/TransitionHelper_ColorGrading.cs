using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Unity PostProcessing settings cannot be animated.
/// This scripts provides neede values for the color grading effect to be animated by an animator.
/// </summary>
public class TransitionHelper_ColorGrading : MonoBehaviour
{
    internal ColorGrading colorGrading;

    [SerializeField] [Tooltip("Set the white balance to a custom color temperature.")]
    private float temperature = 0.0f;

    [SerializeField] [Tooltip("Adjust the overall range of tonal values.")]
    private float contrast = 0.0f;

    void Start()
    {
        // Get actual post processing volume (which cannot be animated itself)
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    /// <summary>
    /// Animator updates values, so post processing volume can be updated in LateUpdate
    /// </summary>
    void LateUpdate()
    {
        colorGrading.temperature.value = temperature;
        colorGrading.contrast.value = contrast;
    }
}