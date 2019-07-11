using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    private Renderer renderer;
    private float t;
    public float speed = 1;

    private bool appear;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<Renderer>();
        t = 0;
        renderer.material.SetFloat("_Visibility", 0);
        appear = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (appear)
        {
            if (FadeIn())
            {
                t = 0;
                appear = false;
            }
        }
        else
        {
            if (FadeOut())
            {
                t = 0;
                appear = true;
            }
        }
    }

    bool FadeIn()
    {
        float val = Mathf.Lerp(0, 1, t);
        t += speed * Time.deltaTime;
        renderer.material.SetFloat("_Visibility", val);
        return t > 1;
    }

    bool FadeOut()
    {
        float val = Mathf.Lerp(1, 0, t);
        t += speed * Time.deltaTime;
        renderer.material.SetFloat("_Visibility", val);
        return t > 1;
    }
}
