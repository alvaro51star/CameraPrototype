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

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
    }

    private void UpdateBar(float currentTime, float maxTimeLooked)
    {
        fill.fillAmount = currentTime / maxTimeLooked;
    }
}
