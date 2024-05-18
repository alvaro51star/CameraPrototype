using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStatusTextUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        EventManager.OnStatusChange += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.OnStatusChange -= UpdateText;
    }

    private void UpdateText(string text)
    {
        _text.text = text;
    }
}
