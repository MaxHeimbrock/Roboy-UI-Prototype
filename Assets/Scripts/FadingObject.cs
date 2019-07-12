using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    private Renderer renderer;
    private float t;
    public float speed = 1;

    private bool appear;
    private bool disappear;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<Renderer>();
        t = 0;
        renderer.material.SetFloat("_Visibility", 0);
        appear = true;
        timer = 0.0f;
        Debug.Log("Running");
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 3)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (appear)
            {
                if (FadeIn())
                {
                    t = 0;
                    appear = false;
                    timer = 0;
                }
            }
            else
            {
                if (FadeOut())
                {
                    t = 0;
                    appear = true;
                    timer = 0;
                }
            }
        }
    }

    bool FadeIn()
    {
        float val = Mathf.Lerp(0, 1, t);
        t += speed * Time.deltaTime;
        renderer.material.SetFloat("_Visibility", val);
        Debug.Log("Executing FadeIn");
        return t > 1;
    }

    bool FadeOut()
    {
        float val = Mathf.Lerp(0, 1, t);
        t += speed * Time.deltaTime;
        renderer.material.SetFloat("_Visibility", 1.0f - val);
        Debug.Log("Executing FadeOutNew");
        return t > 1;
    }
}
