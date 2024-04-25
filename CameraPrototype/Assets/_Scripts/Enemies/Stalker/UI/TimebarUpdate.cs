using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimebarUpdate : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image fill;

    [SerializeField] private float currentTimeLooked;

    private void OnEnable()
    {
        EventManager.OnTimeAdded += UpdateBar;
    }

    private void OnDisable()
    {
        EventManager.OnTimeAdded -= UpdateBar;
    }

    private void Start()
    {
        fill.fillAmount = 0;
    }

    private void UpdateBar(float currentTime, float maxTimeLooked)
    {
        fill.fillAmount = currentTime / maxTimeLooked;
    }
}
