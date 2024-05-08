using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int intensityLevel = 0;
    private int tempValue;

    [SerializeField] private float timeToEnterFirstLevel = 120f;
    [SerializeField] private float timeToEnterSecondLevel = 300f;
    [SerializeField] private float timeToEnterThirdLevel = 480f;
    private float currentTime = 0f;

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
        tempValue = intensityLevel;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToEnterFirstLevel)
        {
            intensityLevel = 1;
        }
        else if (currentTime >= timeToEnterSecondLevel)
        {
            intensityLevel = 2;
        }
        else if (currentTime >= timeToEnterThirdLevel)
        {
            intensityLevel = 3;
        }

        if (tempValue != intensityLevel)
        {
            EventManager.OnLevelIntensityChange.Invoke(intensityLevel);
            tempValue = intensityLevel;
        }
    }
}
