using System.Collections;
using System.Collections.Generic;
using Bhaptics.Tact.Unity;
using UnityEngine;

public class VestTransition : MonoBehaviour
{
    public TactSource[] Sources;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            playTact();
        }
    }

    public void playTact() {
        StartCoroutine(playTactSources());
    }

    IEnumerator playTactSources() {
        foreach (var tactSource in Sources)
        {
            tactSource.Play();
            yield return new WaitForSeconds(0.025f);
        }
    }
}
