using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionHelper_ChromaticAberration : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;

    [SerializeField]
    private float intensity;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        chromaticAberration.intensity.value = intensity;
    }
}
