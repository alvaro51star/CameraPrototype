using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [FormerlySerializedAs("timesInPlaytest")] public List<string> strL_timesInPlaytest;
    
    public bool isEnemyActive = false;

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

    private void OnEnable()
    {
        EventManager.OnEnemyRevealed += SetTrueEnemyActive;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyRevealed -= SetTrueEnemyActive;
    }

    public void AddTimeToList(String time)
    {
        strL_timesInPlaytest.Add(time);
    }

    public void CopyTimeToClipboard()
    {
        System.Text.StringBuilder times = new();

        foreach (var time in strL_timesInPlaytest)
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

    private void SetTrueEnemyActive()
    {
        isEnemyActive = true;
    }
    
}
