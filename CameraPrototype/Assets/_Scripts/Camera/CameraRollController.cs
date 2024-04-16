using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraRollController : MonoBehaviour
{
    [Header("Carrete UI")]
    public int availablePhotos;
    [SerializeField] private int maxPhotos;
    [SerializeField] private TMP_Text availablePhotosTMP;
    [SerializeField] private TMP_Text maxPhotosTMP;

    private void OnEnable()
    {
        EventManager.TakingPhoto += OnTakingPhoto;
    }

    private void OnDisable()
    {
        EventManager.TakingPhoto -= OnTakingPhoto;
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
            EventManager.NoPhotosLeft?.Invoke();
        }

    }

    public void AddRoll(int cameraRoll)
    {
        if(availablePhotos <= 0)
        {
            EventManager.CanTakePhotosAgain?.Invoke();
        }
        availablePhotos += cameraRoll;
        availablePhotos = Mathf.Clamp(availablePhotos, 0, maxPhotos);
        availablePhotosTMP.text = availablePhotos.ToString();
    }
}
