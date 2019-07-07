using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionHelper_AutoExposure : MonoBehaviour
{
    private AutoExposure autoExposure;

    [SerializeField]
    private float exposureCompensation = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out autoExposure);
    }

    private void LateUpdate()
    {
        autoExposure.keyValue.value = exposureCompensation;
    }
}
