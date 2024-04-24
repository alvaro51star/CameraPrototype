using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float playtestTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            playtestTime += Time.deltaTime;
        }
    }

    public void CopyTimeToClipboard()
    {
        TimeSpan time = TimeSpan.FromSeconds(playtestTime);
        string timeString = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        CopyToClipboard(timeString);
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
