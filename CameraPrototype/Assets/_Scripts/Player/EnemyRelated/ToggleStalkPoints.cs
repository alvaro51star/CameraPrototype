using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStalkPoints : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        if (!playerTransform)
        {
            Debug.LogError("Jugador no encontrado, asegurese de que hay un jugador en la escena");
        }
    }

    private void Update()
    {
        transform.position = playerTransform.position;
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
