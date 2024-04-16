using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image fill;

    private void OnEnable()
    {
        EventManager.OnTimeAdded += UpdateBar;
    }

    private void OnDisable()
    {
        EventManager.OnTimeAdded -= UpdateBar;
    }

    // Update is called once per frame
    void Update()
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
