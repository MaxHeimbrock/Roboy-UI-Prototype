using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


/// <summary>
/// Unity PostProcessing settings cannot be animated.
/// This scripts provides neede values for the depth of field effect to be animated by an animator.
/// </summary>
public class TransitionHelper_DepthOfField : MonoBehaviour
{
    internal DepthOfField depthOfField;

    [SerializeField] [Tooltip("Set the distance to the point of focus.")]
    private float focusDistance = 10.0f;

    [SerializeField]
    [Tooltip(
        "Set the ratio of the aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
    private float aperture = 32.0f;

    [SerializeField]
    [Tooltip(
        "Set the distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
    private float focalLength = 22.0f;

    private void Start()
    {
        // Get actual post processing volume (which cannot be animated itself)
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    /// <summary>
    /// Animator updates values, so post processing volume can be updated in LateUpdate
    /// </summary>
    private void LateUpdate()
    {
        depthOfField.focusDistance.value = focusDistance;
        depthOfField.aperture.value = aperture;
        depthOfField.focalLength.value = focalLength;
    }
}