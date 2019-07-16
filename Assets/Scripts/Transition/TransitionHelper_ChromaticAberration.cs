using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the chromatic aberration effect to be animated by an animator.
 */
public class TransitionHelper_ChromaticAberration : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;

    [SerializeField]
    private float intensity;

    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    void LateUpdate()
    {
        chromaticAberration.intensity.value = intensity;
    }
}
