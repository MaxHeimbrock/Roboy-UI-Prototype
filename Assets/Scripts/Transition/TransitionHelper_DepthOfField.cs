using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

    private void LateUpdate()
    {
        depthOfField.focusDistance.value = focusDistance;
        depthOfField.aperture.value = aperture;
        depthOfField.focalLength.value = focalLength;
    }
}
