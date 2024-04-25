using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStatusTextUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject player;

    private void OnEnable()
    {
        EventManager.OnStatusChange += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.OnStatusChange -= UpdateText;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void UpdateText(string text)
    {
        _text.text = text;
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
    }
}
