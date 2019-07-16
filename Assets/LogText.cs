using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    private string text = "No Log";

    private int lines = 0;

    private TextMesh textMesh;

    public void Start()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
    }

    public void addToLogText(string message)
    {
        text += message;
        text += "\n";

        lines++;
    }
}
