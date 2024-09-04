using MoreMountains.Tools;
using UnityEngine;

[RequireComponent(typeof(AnomaliesData))]
public class AnomalyBehaviour : MonoBehaviour
{
    private Camera _anomalyCamera;
    private AnomaliesData _anomaliesData;
    [SerializeField] private Renderer anomalyRenderer;
    [SerializeField] private AnomalyCullingGroup anomalyCullingGroup;
    [HideInInspector] public bool isInPlayersTrigger;

    private void OnEnable()
    {
        EventManager.OnTakingPhoto += OnTakingPhoto;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnTakingPhoto;
    }

    private void Awake()
    {
        _anomalyCamera = GameObject.FindGameObjectWithTag("AnomalyCamera").GetComponent<Camera>();
        //can't find this reference on start because this camera is disabled on start (AnomalyCameraManager)
    }

    protected virtual void Start()
    {
        _anomaliesData = GetComponent<AnomaliesData>();
        
        if(anomalyRenderer)
            return;
        anomalyRenderer = GetComponent<Renderer>();
        if(anomalyRenderer)
            return;
        Debug.LogError("Falta poner el renderer en anomalyRenderer a " + gameObject);
    }

    private void OnTakingPhoto() //checks conditions to reveal the anomaly
    {
        //if (!isInPlayersTrigger) //value changed by player's trigger (AnomalyCamTrigger)
           // return;
           

        if (!_anomaliesData.isActiveAndEnabled)
            return;


        if (!_anomalyCamera)
        {
            _anomalyCamera = GameObject.FindGameObjectWithTag("AnomalyCamera").GetComponent<Camera>();
            //in case it loses the reference
        }

        if (!_anomalyCamera.isActiveAndEnabled)
            return;
          
        //if(!anomalyRenderer.isVisible)
         //   return;

        Debug.Log( gameObject + " "+ IsMeshVisibleToCamera(_anomalyCamera, anomalyRenderer));
        if (!IsMeshVisibleToCamera(_anomalyCamera, anomalyRenderer))
            return;
          
        //if(!_anomaliesData.cullingIsVisible)
          //   return;
        
        
        Debug.Log(gameObject + " is visible");
        PhotoAction();
    }

    public virtual void PhotoAction()
    {
        RevealAnomaly(_anomaliesData);
    }

    protected void RevealAnomaly(AnomaliesData anomalyToReveal)
    {
        if (anomalyToReveal.revealType)
        {
            anomalyToReveal.transform.ChangeLayersRecursively(LayerMask.NameToLayer("Default"));
            anomalyToReveal.anomalyCollider.enabled = true;
        }
        
        else
        {
            anomalyToReveal.gameObject.SetActive(false);
        }
        
        _anomaliesData.enabled = false;
        this.enabled = false;
    }


    /*private bool IsVisible()//check if collider is in viewport
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_anomalyCamera);

        if (GeometryUtility.TestPlanesAABB(planes , _anomaliesData.anomalyCollider.bounds))
            return true;
        else
            return false;
    }*/
    
    ///<summary>
    /// Checks if mesh is in viewport
    /// </summary>
    private bool IsMeshVisibleToCamera(Camera anomalyCamera, Renderer anomalyRender)
    {
        // easy part first. if the renderer's pivot is in view we should be ok
        if (IsVisible(anomalyRender.transform.position) /*&& !IsOccluded(anomalyRender.transform.position)*/)
            return true;

        // now the more tricky part. if the pivot is not in view,
        // the mesh may still have some bits that are visible
        // let's check each of the 8 points and see if one is visible
        for (var i = 0; i < 8; i++)
            if (IsVisible(GetPointFromBits(i)))
            {
                /*if(!IsOccluded(GetPointFromBits(i)))*/
                    return true;
            }

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
            var viewPos = anomalyCamera.WorldToViewportPoint(point);
            return (viewPos is { x: >= 0f, y: >= 0f } && viewPos.x <= 1f && viewPos.y <= 1f);
        }
        // Just a simple helper to get the point that I want
        Vector3 GetPoint(int xDir, int yDir, int zDir)
        {
            var bounds = anomalyRender.bounds;
            return new Vector3(
                bounds.center.x + (bounds.extents.x * xDir),
                bounds.center.y + (bounds.extents.y * yDir),
                bounds.center.z + (bounds.extents.z * zDir));
        }

        /*bool IsOccluded(Vector3 point)
        {
            return Physics.Raycast(anomalyCamera.transform.position, point, anomalyCamera.farClipPlane);
        }*/
    }
}