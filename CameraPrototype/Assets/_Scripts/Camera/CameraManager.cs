using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    //[SerializeField] private GameObject anomaliesCameraGO;
    //[SerializeField] private GameObject pastCameraGO;

    [SerializeField] private PhotoCapture m_photoCapture;
    [FormerlySerializedAs("cameraUI")] [SerializeField] private GameObject m_GO_cameraUI;

    [Header("Camera lens")]
    [SerializeField] private List<Camera> m_list_cameras;
    private int m_selectedCamera;
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
        foreach (var camera1 in m_list_cameras)
        {
            if (i == m_selectedCamera)
            {
                camera1.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void OnUsingCamera()
    {
        //camera feedback
        m_photoCapture.SetHasCameraEquipped(true);
        m_GO_cameraUI.SetActive(true);
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
        m_photoCapture.SetHasCameraEquipped(false);
        m_GO_cameraUI.SetActive(false);
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
        foreach (var camera1 in m_list_cameras)
        {
            if (i == m_selectedCamera)
            {
                camera1.gameObject.SetActive(true);
                //print(camera1 + " activated");
            }
            else
                camera1.gameObject.SetActive(false);
            i++;
        }
    }

    private void DisableSelectedLens()
    {
        int i = 0;
        foreach (var camera1 in m_list_cameras)
        {
            if (i == m_selectedCamera)
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
            if (m_selectedCamera == m_list_cameras.Count - 1)
                m_selectedCamera = 0;
            else
                m_selectedCamera++;
        }
        else if(inputValue < 0)
        {
            if (m_selectedCamera == 0)
                m_selectedCamera = m_list_cameras.Count - 1;
            else
                m_selectedCamera--;
        }
    }

    public int GetActiveCameraIndex()
    {
        return m_selectedCamera;
    }

    public List<Camera> GetPhotoCameras()
    {
        return m_list_cameras;
    }
    
}
