using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the vignette effect to be animated by an animator.
 */
public class TransitionHelper_Vignette : MonoBehaviour
{
    private Vignette vignette;

    [SerializeField]
    private float intensity = 0.0f;
    [SerializeField]
    private float smoothness = 0.3f;

    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    void LateUpdate()
    {
        vignette.intensity.value = intensity;
        vignette.smoothness.value = smoothness;
    }
}
