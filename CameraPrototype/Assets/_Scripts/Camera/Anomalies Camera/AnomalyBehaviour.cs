using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnomaliesData))]
public class AnomalyBehaviour : MonoBehaviour
{
    private Camera anomalyCamera;
    private AnomaliesData anomaliesData;
    public bool isInPlayersTrigger;
    private void OnEnable()
    {
        EventManager.OnTakingPhoto += OnTakingPhoto;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnTakingPhoto;
    }

    private void Start()
    {
        anomaliesData = GetComponent<AnomaliesData>();
    }    
    
    private void OnTakingPhoto()//checks conditions to reveal the anomaly
    {
        if(!isInPlayersTrigger)
            return;

        anomalyCamera = GameObject.FindGameObjectWithTag("AnomalyCamera").GetComponent<Camera>();
        
        if(!anomalyCamera)
            return;

        if(!anomaliesData)
            return;

        if (!anomaliesData.isActiveAndEnabled)
            return;

        if (!MeshIsVisibleToCamera(anomalyCamera, gameObject.GetComponent<Renderer>()))
            return;

        RevealAnomaly();
    }
    private void RevealAnomaly()
    {
        if (anomaliesData.revealType)
        {
            if (transform.CompareTag("Enemy"))
            {
                gameObject.GetComponent<StalkerBehaviour>().ActivateCollision();
            }

            gameObject.layer = LayerMask.NameToLayer("Default");
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Default"));
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Default");
            }
            if (GetComponent<StalkerBehaviour>())
            {
                GetComponent<StalkerBehaviour>().enabled = true;
            }
            else if (GetComponent<Llave>())
            {
                GetComponent<Llave>().enabled = true;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        anomaliesData.enabled = false;
        this.enabled = false;

    }

    private bool MeshIsVisibleToCamera(Camera camera, Renderer renderer)
    {
        // easy part first. if the renderer's pivot is in view we should be ok
        if (IsVisible(renderer.transform.position))
            return true;

        // now the more tricky part. if the pivot is not in view,
        // the mesh may still have some bits that are visible
        // let's check each of the 8 points and see if one is visible
        for (var i = 0; i < 8; i++)
            if (IsVisible(GetPointFromBits(i)))
                return true;

        // no points are visible
        return false;

        // bit shifting
        Vector3 GetPointFromBits(int bits)
        {
            var xDir = (int)(((bits >> 2) & 0x1) * 2f - 1f); // get the bit and remap
            var yDir = (int)(((bits >> 1) & 0x1) * 2f - 1f); // get the bit and remap
            var zDir = (int)(((bits >> 0) & 0x1) * 2f - 1f); // get the bit and remap
            return GetPoint(xDir, yDir, zDir);
        }
        bool IsVisible(Vector3 point)
        {
            var viewPos = camera.WorldToViewportPoint(point);
            return (viewPos.x >= 0f && viewPos.y >= 0f && viewPos.x <= 1f && viewPos.y <= 1f);
        }
        // Just a simple helper to get the point that I want
        Vector3 GetPoint(int xDir, int yDir, int zDir)
        {
            var bounds = renderer.bounds;
            return new Vector3(
                bounds.center.x + (bounds.extents.x * xDir),
                bounds.center.y + (bounds.extents.y * yDir),
                bounds.center.z + (bounds.extents.z * zDir));
        }
    }

}
