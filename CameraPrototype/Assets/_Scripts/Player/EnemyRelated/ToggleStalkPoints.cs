using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToggleStalkPoints : MonoBehaviour
{
    [FormerlySerializedAs("playerTransform")] [SerializeField] private Transform m_tf_playerTransform;

    private void Start()
    {
        m_tf_playerTransform = FindObjectOfType<PlayerMovement>().transform;
        if (!m_tf_playerTransform)
        {
            Debug.LogError("Jugador no encontrado, asegurese de que hay un jugador en la escena");
        }
    }

    private void Update()
    {
        if(m_tf_playerTransform){
            transform.position = m_tf_playerTransform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPoint))
        {
            //Debug.Log("Localizao");
            //TODO comprobar que esté en visión

            // Vector3 direction = transform.position - stalkPoint.transform.position;
            // float distance = direction.magnitude;
            // if (Physics.Raycast(transform.position, direction, out RaycastHit ray, distance))
            // {
            //     if (ray.transform.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPointBehaviour))
            //     {

            //     }
            // }

            stalkPoint.TogglePosition(true);
            StalkPointsManager.instance.ModifyStalkPointList(stalkPoint, true);

        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPoint))
        {
            Debug.Log("Perdido");
            stalkPoint.TogglePosition(false);
            StalkPointsManager.instance.ModifyStalkPointList(stalkPoint, false);
        }
    }
}
