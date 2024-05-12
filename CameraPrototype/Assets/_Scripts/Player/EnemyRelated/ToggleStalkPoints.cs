using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStalkPoints : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPoint))
        {
            Debug.Log("Localizao");
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

    private void Update()
    {
        transform.position = playerTransform.position;
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
