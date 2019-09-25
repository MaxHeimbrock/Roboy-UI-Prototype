using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Unity PostProcessing settings cannot be animated.
/// This scripts provides neede values for the chromatic abberation effect to be animated by an animator.
/// </summary>
public class TransitionHelper_ChromaticAberration : MonoBehaviour
{
    internal ChromaticAberration chromaticAberration;

    [SerializeField] [Tooltip("Set the strength of the Chromatic Aberration effect.")]
    private float intensity;

    void Start()
    {
        // Get actual post processing volume (which cannot be animated itself)
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
    }

    /// <summary>
    /// Animator updates values, so post processing volume can be updated in LateUpdate
    /// </summary>
    void LateUpdate()
    {
        chromaticAberration.intensity.value = intensity;
    }
}