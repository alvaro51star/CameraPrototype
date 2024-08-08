using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<string> timesInPlaytest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void AddTimeToList(String time)
    {
        timesInPlaytest.Add(time);
    }

    public void CopyTimeToClipboard()
    {
        System.Text.StringBuilder times = new();

        foreach (var time in timesInPlaytest)
        {
            times.AppendLine(time);
        }

        CopyToClipboard(times.ToString());
    }

    public void CopyToClipboard(string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    private void OnApplicationQuit()
    {
        CopyTimeToClipboard();
    }
    
}
