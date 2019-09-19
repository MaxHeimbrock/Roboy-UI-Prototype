using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    public CustomSlider slider;

    public Light[] lights;
    public ReflectionProbe[] reflectionProbes;
    public float[] lightIntensities;
    public float[] reflectionProbesIntensities;
    public float skyboxExposure;

    private void Start()
    {
        lights = (Light[]) Resources.FindObjectsOfTypeAll(typeof(Light));
        reflectionProbes = (ReflectionProbe[])Resources.FindObjectsOfTypeAll(typeof(ReflectionProbe));
        skyboxExposure = RenderSettings.skybox.GetFloat("_Exposure");

        lightIntensities = new float[lights.Length];
        reflectionProbesIntensities = new float[reflectionProbes.Length];

        for (int i = 0; i < lights.Length; i++)
        {
            lightIntensities[i] = lights[i].intensity;
        }
        for (int i = 0; i < reflectionProbes.Length; i++)
        {
            reflectionProbesIntensities[i] = reflectionProbes[i].intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = slider.GetValue();
        skyboxExposure = RenderSettings.skybox.GetFloat("_Exposure");
        RenderSettings.skybox.SetFloat("_Exposure", skyboxExposure * value);
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = lightIntensities[i] * value;
        }
        for(int i = 0; i < reflectionProbes.Length; i++)
        {
            reflectionProbes[i].intensity = reflectionProbesIntensities[i] * value;
        }
    }
}
