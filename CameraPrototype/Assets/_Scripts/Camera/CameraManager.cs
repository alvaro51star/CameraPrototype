using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private GameObject anomaliesCameraGO;
    [SerializeField] private GameObject pastCameraGO;

    [FormerlySerializedAs("m_photoCapture")] [SerializeField] private PhotoCapture _photoCapture;
    [FormerlySerializedAs("CameraUI")] [SerializeField] private GameObject _cameraUI;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnNotUsingCamera -= OnNotUsingCamera;
    }
    
    private void Start()
    {
        anomaliesCameraGO.SetActive(false);
        if(!pastCameraGO)
            return;
        pastCameraGO.SetActive(false);
    }

    private void OnUsingCamera()
    {
        //camera feedback
        _photoCapture.SetHasCameraEquiped(true);
        _cameraUI.SetActive(true);
        UIManager.instance.SetPointersActive(false);
        
        //camera management
        if (!anomaliesCameraGO)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");//in case it loses the reference
        }
        if (anomaliesCameraGO && !anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(true);
        }

    }

    private void OnNotUsingCamera()
    {
        //camera feedback
        _photoCapture.SetHasCameraEquiped(false);
        _cameraUI.SetActive(false);
        if (!UIManager.instance.GetIsReading())
        {
            UIManager.instance.SetPointersActive(true);
        }
        
        //camera management
        if (!anomaliesCameraGO)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");
        }
        if (anomaliesCameraGO && !anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(false);
        }
    }
    
}
