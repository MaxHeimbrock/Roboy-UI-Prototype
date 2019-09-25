using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Unity PostProcessing settings cannot be animated.
/// This scripts provides neede values for the auto exposure effect to be animated by an animator.
/// </summary>
public class TransitionHelper_AutoExposure : MonoBehaviour
{
    internal AutoExposure autoExposure;

    [SerializeField]
    [Tooltip("Set the middle-grey value to compensate the global exposure of the scene.")]
    private float exposureCompensation = 1.0f;
    [SerializeField]
    [Tooltip("Set the minimum average luminance to consider for auto exposure in EV.")]
    private float minimum_ev = 0.0f;
    [SerializeField]
    [Tooltip("Set the maximum average luminance to consider for auto exposure in EV.")]
    private float maximum_ev = 0.0f;
    
    void Start()
    {
        // Get actual post processing volume (which cannot be animated itself)
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out autoExposure);
    }

    /// <summary>
    /// Animator updates values, so post processing volume can be updated in LateUpdate
    /// </summary>
    private void LateUpdate()
    {
        autoExposure.keyValue.value = exposureCompensation;
        autoExposure.minLuminance.value = minimum_ev;
        autoExposure.maxLuminance.value = maximum_ev;
    }
}
