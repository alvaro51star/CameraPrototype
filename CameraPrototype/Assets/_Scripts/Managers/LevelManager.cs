using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int intensityLevel = 0;
    private int m_tempValue;

    public float timeToEnterFirstLevel = 120f;
    public float timeToEnterSecondLevel = 300f;
    public float timeToEnterThirdLevel = 480f;
    
    private float m_currentTime = 0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        m_tempValue = intensityLevel;
    }

    private void Update()
    {
        m_currentTime += Time.deltaTime;

        if (m_currentTime >= timeToEnterFirstLevel)
        {
            intensityLevel = 1;
        }
        else if (m_currentTime >= timeToEnterSecondLevel)
        {
            intensityLevel = 2;
        }
        else if (m_currentTime >= timeToEnterThirdLevel)
        {
            intensityLevel = 3;
        }

        if (m_tempValue != intensityLevel)
        {
            EventManager.OnLevelIntensityChange?.Invoke(intensityLevel);
            m_tempValue = intensityLevel;
        }
    }
}
