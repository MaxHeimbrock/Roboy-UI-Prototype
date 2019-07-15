using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


/**
 * Unity PostProcessing settings cannot be animated.
 * This scripts provides neede values for the depth of field effect to be animated by an animator.
 */
public class TransitionHelper_DepthOfField : MonoBehaviour
{
    private DepthOfField depthOfField;

    [SerializeField]
    private float focusDistance = 10.0f;
    [SerializeField]
    private float aperture = 32.0f;
    [SerializeField]
    private float focalLength = 22.0f;

    private void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    /**
     * Animator updates values, so post processing volume can be updated in LateUpdate
     */
    private void LateUpdate()
    {
        depthOfField.focusDistance.value = focusDistance;
        depthOfField.aperture.value = aperture;
        depthOfField.focalLength.value = focalLength;
    }
}
