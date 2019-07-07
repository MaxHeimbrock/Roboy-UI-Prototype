using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TransitionHelper_Bloom : MonoBehaviour
{
    private Bloom bloom;

    [SerializeField]
    private float intensity;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bloom.intensity.value = intensity;
    }
}
