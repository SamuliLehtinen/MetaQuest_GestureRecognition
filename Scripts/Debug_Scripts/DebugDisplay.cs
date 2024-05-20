using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script used to display the debug logs on the console attached to an OVRHand game object
/// </summary>
public class DebugDisplay : MonoBehaviour
{
    Dictionary<string, string> debugLogs = new Dictionary<string, string>();
    public TextMeshProUGUI display;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            string[] splitString = logString.Split(char.Parse(":"));
            string debugKey = splitString[0];
            if(debugKey.Contains("PA"))
            {
                string debugValue = splitString.Length > 1 ? splitString[1] : "";
            
                if (debugLogs.ContainsKey(debugKey))
                {
                    debugLogs[debugKey] = debugValue;
                }
                else
                {
                    debugLogs.Add(debugKey, debugValue);
                }
            }
        }
        string displayText = "";

        foreach (KeyValuePair<string, string> log in debugLogs)
        {
            if (log.Value == "")
            {
                displayText += log.Key + "\n";
            }
            else
            {
                displayText += log.Key + ": " + log.Value + "\n";
            }
        }
        display.text = displayText;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PA time :" + Time.time);
        Debug.Log("PA name :" + gameObject.name);
    }
}
