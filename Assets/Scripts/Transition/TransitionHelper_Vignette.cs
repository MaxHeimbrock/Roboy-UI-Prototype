using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionHelper_Vignette : MonoBehaviour
{
    private Vignette vignette;

    [SerializeField]
    private float intensity = 0.0f;
    [SerializeField]
    private float smoothness = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        vignette.intensity.value = intensity;
        vignette.smoothness.value = smoothness;
    }
}
