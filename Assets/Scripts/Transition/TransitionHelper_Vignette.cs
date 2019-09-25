using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Unity PostProcessing settings cannot be animated.
/// This scripts provides neede values for the vignette effect to be animated by an animator.
/// </summary>
public class TransitionHelper_Vignette : MonoBehaviour
{
    private Vignette vignette;

    [SerializeField] [Tooltip("Set the amount of vignetting on screen.")]
    private float intensity = 0.0f;

    [SerializeField] [Tooltip("Set the smoothness of the Vignette borders.")]
    private float smoothness = 0.3f;

    void Start()
    {
        // Get actual post processing volume (which cannot be animated itself)
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    /// <summary>
    /// Animator updates values, so post processing volume can be updated in LateUpdate
    /// </summary>
    void LateUpdate()
    {
        vignette.intensity.value = intensity;
        vignette.smoothness.value = smoothness;
    }
}