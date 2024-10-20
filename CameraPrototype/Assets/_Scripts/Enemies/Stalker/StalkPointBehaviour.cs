using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StalkPointBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("position")] [SerializeField] private Transform m_Tf_Position;
    [FormerlySerializedAs("isActive")] public bool iIsActive = false;

    private void Start()
    {
        m_Tf_Position.gameObject.SetActive(iIsActive);
    }

    public void TogglePosition(bool active)
    {
        iIsActive = active;
        m_Tf_Position.gameObject.SetActive(iIsActive);
    }
}
