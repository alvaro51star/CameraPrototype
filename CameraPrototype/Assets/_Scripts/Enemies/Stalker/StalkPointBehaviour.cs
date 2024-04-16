using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkPointBehaviour : MonoBehaviour
{
    [SerializeField] private Transform position;
    public bool isActive = false;

    private void Start()
    {
        position.gameObject.SetActive(isActive);
    }

    public void TogglePosition(bool active)
    {
        isActive = active;
        position.gameObject.SetActive(isActive);
    }
}
