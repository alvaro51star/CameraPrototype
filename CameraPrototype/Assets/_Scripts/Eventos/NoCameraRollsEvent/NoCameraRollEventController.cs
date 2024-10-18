using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
public class NoCameraRollEventController : MonoBehaviour
{
    [Header("Variables modificables")]
    [SerializeField] private float timeToGetRoll = 15f;
    [Space]
    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timerGameObject;

    private bool isEventActive = false;
    private float timeLeft;

    [SerializeField] private DOTweenAnimation changeTextAnimation;
    
    private Enemy _enemy;
    private bool _isKilling = false;
    
    
    private void Start()
    {
        timeLeft = timeToGetRoll;
        UpdateTimer(timeLeft);
        _enemy = FindObjectOfType<Enemy>();
    }


    private void OnEnable()
    {
        EventManager.OnRollFinished += StartEvent;
        EventManager.OnAddRoll += StopEvent;
    }

    private void OnDisable()
    {
        EventManager.OnRollFinished -= StartEvent;
        EventManager.OnAddRoll -= StopEvent;
    }

    private void StartEvent()
    {
        if (isEventActive)
            return;

        isEventActive = true;
        //Aqui irian los efectos
        
    }

    private void StopEvent(int rolls)
    {
        if(!isEventActive)
            return;

        isEventActive = false;
        timeLeft = timeToGetRoll;
        //Aqui se paran los efectos
    }

    private void Update()
    {
        if (isEventActive && GameManager.Instance.isEnemyActive)
        {
            timerGameObject.SetActive(true);
            
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else if(!_isKilling)
            {
                Debug.Log("Time is up!");
                timeLeft = 0;
                _enemy.KillPlayer();
                _isKilling = true;
            }
        }
        else
        {
            timerGameObject.SetActive(false);
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        
        timerText.text = $"{minutes:00} : {seconds:00}";
        
        // changeTextAnimation.endValueString = $"{minutes:00} : {seconds:00}";
        // changeTextAnimation.DOPlay();
    }
}
