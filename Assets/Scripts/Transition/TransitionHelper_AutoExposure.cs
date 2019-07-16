using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the auto exposure effect to be animated by an animator.
 */
public class TransitionHelper_AutoExposure : MonoBehaviour
{
    private AutoExposure autoExposure;

    [SerializeField]
    private float exposureCompensation = 1.0f;

    
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out autoExposure);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    private void LateUpdate()
    {
        autoExposure.keyValue.value = exposureCompensation;
    }
}
