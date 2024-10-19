using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRollController : MonoBehaviour
{
    [FormerlySerializedAs("photoCapture")] [SerializeField] private PhotoCapture m_photoCapture;
    [FormerlySerializedAs("textForDialogue")] [SerializeField] private TextForDialogue m_textForDialogue;
    [Header("Carrete UI")]
    public int availablePhotos;
    [FormerlySerializedAs("maxPhotos")] [SerializeField] private int m_maxPhotos;
    [FormerlySerializedAs("availablePhotosTMP")] [SerializeField] private TMP_Text m_TMPtxt_availablePhotos;
    [FormerlySerializedAs("maxPhotosTMP")] [SerializeField] private TMP_Text m_TMPtxt_maxPhotos;

    private void OnEnable()
    {
        EventManager.OnTakingPhoto += RemoveRoll;
        EventManager.OnAddRoll += AddRoll;
    }

    private void OnDisable()
    {
        EventManager.OnTakingPhoto -= RemoveRoll;
        EventManager.OnAddRoll -= AddRoll;
    }
    private void Start()
    {
        availablePhotos = m_maxPhotos;

        m_TMPtxt_availablePhotos.text = availablePhotos.ToString();
        m_TMPtxt_maxPhotos.text = m_maxPhotos.ToString();
    }

    private void RemoveRoll()
    {
        availablePhotos--;
        availablePhotos = Mathf.Clamp(availablePhotos, 0, m_maxPhotos);
        m_TMPtxt_availablePhotos.text = availablePhotos.ToString();
        
        if(availablePhotos <= 0)
        {
            m_TMPtxt_availablePhotos.color = Color.red;
            m_photoCapture.isAbleToTakePhoto = false;
            EventManager.OnRollFinished?.Invoke();
            m_textForDialogue.StartDialogue();
        }
    }

    public void AddRoll(int cameraRoll)
    {
        print("roll + " + cameraRoll);
        if(availablePhotos <= 0)
        {
            m_photoCapture.isAbleToTakePhoto = true;
            m_TMPtxt_availablePhotos.color = Color.white;
        }
        availablePhotos += cameraRoll;
        availablePhotos = Mathf.Clamp(availablePhotos, 0, m_maxPhotos);
        m_TMPtxt_availablePhotos.text = availablePhotos.ToString();
    }
}
