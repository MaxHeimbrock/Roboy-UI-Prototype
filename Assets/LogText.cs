using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    private string text = "Start Log";

    public TextMeshProUGUI textMesh;

    public void Start()
    {
        textMesh.SetText(text);
    }

    public void addToLogText(string message)
    {
        text = message + "\n" + text;
        textMesh.SetText(text);
    }
}
