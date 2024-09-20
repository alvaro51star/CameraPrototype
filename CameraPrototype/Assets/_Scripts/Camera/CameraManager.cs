using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [FormerlySerializedAs("_photoCapture")]
    [Header("Cameras")]
    //[SerializeField] private GameObject anomaliesCameraGO;
    //[SerializeField] private GameObject pastCameraGO;

    [FormerlySerializedAs("m_photoCapture")] [SerializeField] private PhotoCapture photoCapture;
    [FormerlySerializedAs("_cameraUI")] [FormerlySerializedAs("CameraUI")] [SerializeField] private GameObject cameraUI;

    [Header("Camera lens")]
    [SerializeField] private List<Camera> cameras;
    private int _selectedCamera;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
        EventManager.OnChangeLens += SetLensValue;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnNotUsingCamera -= OnNotUsingCamera;
        EventManager.OnChangeLens -= SetLensValue;
    }
    
    private void Start()//disable all lens
    {
        /*anomaliesCameraGO.SetActive(false);
        if(!pastCameraGO)
            return;
        pastCameraGO.SetActive(false);*/
        
        int i = 0;
        foreach (var camera1 in cameras)
        {
            if (i == _selectedCamera)
            {
                camera1.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void OnUsingCamera()
    {
        //camera feedback
        photoCapture.SetHasCameraEquiped(true);
        cameraUI.SetActive(true);
        UIManager.instance.SetPointersActive(false);
        
        //camera management
        /*if (!anomaliesCameraGO)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");//in case it loses the reference
        }
        if (anomaliesCameraGO && !anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(true);
        }*/
        ChangeLens();
    }

    private void OnNotUsingCamera()
    {
        //camera feedback
        photoCapture.SetHasCameraEquiped(false);
        cameraUI.SetActive(false);
        if (!UIManager.instance.GetIsReading())
        {
            UIManager.instance.SetPointersActive(true);
        }
        
        //camera management
        /*if (!anomaliesCameraGO)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");//no borro esto por si vuelve a haber errores
        }
        if (anomaliesCameraGO && anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(false);
        }*/
        DisableSelectedLens();
    }

    private void ChangeLens()
    {
        int i = 0;
        foreach (var camera1 in cameras)
        {
            if (i == _selectedCamera)
            {
                camera1.gameObject.SetActive(true);
                print(camera1 + " activated");
            }
            else
                camera1.gameObject.SetActive(false);
            i++;
        }
    }

    private void DisableSelectedLens()
    {
        int i = 0;
        foreach (var camera1 in cameras)
        {
            if (i == _selectedCamera)
            {
                camera1.gameObject.SetActive(false);
                return;
            }
            i++;
        }
    }

    private void SetLensValue(float inputValue)
    {
        if (inputValue > 0)
        {
            if (_selectedCamera == cameras.Count - 1)
                _selectedCamera = 0;
            else
                _selectedCamera++;
        }
        else if(inputValue < 0)
        {
            if (_selectedCamera == 0)
                _selectedCamera = cameras.Count - 1;
            else
                _selectedCamera--;
        }
    }

    public int GetActiveCameraIndex()
    {
        return _selectedCamera;
    }

    public List<Camera> GetPhotoCameras()
    {
        return cameras;
    }
    
}
