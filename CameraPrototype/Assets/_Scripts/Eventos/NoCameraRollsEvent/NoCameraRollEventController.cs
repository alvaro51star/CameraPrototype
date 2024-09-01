using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
public class NoCameraRollEventController : MonoBehaviour
{
    [SerializeField] private bool isEventActive = false;

    [SerializeField] private float timeToGetRoll = 15f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timerGameObject;

    private float timeLeft;

    [SerializeField] private DOTweenAnimation changeTextAnimation;
    
    private Enemy _enemy;
    private bool isKilling = false;
    
    
    private void Start()
    {
        timeLeft = timeToGetRoll;
        UpdateTimer(timeLeft);
        //_enemy = FindObjectOfType<StalkerBehaviour>().GetComponent<IEnemy>();
        _enemy = FindObjectOfType<Enemy>();
        //GameObject.FindFirstObjectByType(IEnemy);
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
        //StartTimer
        
    }

    private void StopEvent(int rolls)
    {
        if(!isEventActive)
            return;

        isEventActive = false;
        //StopAllCoroutines();
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
            else if(!isKilling)
            {
                Debug.Log("Time is up!");
                timeLeft = 0;
                _enemy.KillPlayer();
                isKilling = true;
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
