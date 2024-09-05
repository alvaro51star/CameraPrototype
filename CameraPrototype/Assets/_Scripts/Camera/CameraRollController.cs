using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraRollController : MonoBehaviour
{
    [SerializeField] private PhotoCapture photoCapture;
    [SerializeField] private TextForDialogue textForDialogue;
    [Header("Carrete UI")]
    public int availablePhotos;
    [SerializeField] private int maxPhotos;
    [SerializeField] private TMP_Text availablePhotosTMP;
    [SerializeField] private TMP_Text maxPhotosTMP;

    private void OnEnable()
    {
        EventManager.OnTakingPhoto += OnTakingPhoto;
        EventManager.OnAddRoll += AddRoll;
    }

    private void OnDisable()
    {
        EventManager.OnTakingPhoto -= OnTakingPhoto;
        EventManager.OnAddRoll -= AddRoll;
    }
    private void Start()
    {
        availablePhotos = maxPhotos;

        availablePhotosTMP.text = availablePhotos.ToString();
        maxPhotosTMP.text = maxPhotos.ToString();
    }

    private void OnTakingPhoto()
    {
        availablePhotos--;
        availablePhotos = Mathf.Clamp(availablePhotos, 0, maxPhotos);
        availablePhotosTMP.text = availablePhotos.ToString();
        
        if(availablePhotos <= 0)
        {
            availablePhotosTMP.color = Color.red;
            photoCapture.canTakePhoto = false;
            EventManager.OnRollFinished?.Invoke();
            textForDialogue.StartDialogue();
        }
    }

    public void AddRoll(int cameraRoll)
    {
        print("roll + " + cameraRoll);
        if(availablePhotos <= 0)
        {
            photoCapture.canTakePhoto = true;
            availablePhotosTMP.color = Color.white;
        }
        availablePhotos += cameraRoll;
        availablePhotos = Mathf.Clamp(availablePhotos, 0, maxPhotos);
        availablePhotosTMP.text = availablePhotos.ToString();
    }
}
