using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the bloom effect to be animated by an animator.
 */
public class TransitionHelper_Bloom : MonoBehaviour
{
    private Bloom bloom;

    [SerializeField]
    private float intensity;

    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    void LateUpdate()
    {
        bloom.intensity.value = intensity;
    }
}
