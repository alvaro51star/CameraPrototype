using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCanvasRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();   
    }

    private void LookAtPlayer()
    {
        if (playerTransform != null)
        {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.transform.position.z));
        }
    }
}
