using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private RenderTexture anomaliesCamRT;

    [Header("Carrete UI")]
    [SerializeField] private int maxPhotos;
    [SerializeField] private TMP_Text availablePhotosTMP;
    [SerializeField] private TMP_Text maxPhotosTMP;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;
    [SerializeField] private AudioClip cameraClickClip;
    [SerializeField] private AudioClip noPhotosClip;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool canTakePhoto = true;
    private int availablePhotos;

    private void Start()
    {
        screenCapture = new Texture2D(anomaliesCamRT.width, anomaliesCamRT.height, anomaliesCamRT.graphicsFormat,
                              UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
        //anomaliesCamRT esta a 1920 x 1080, en futuro buscar que se adapte al tamanio de la pantalla
        availablePhotos = maxPhotos;

        availablePhotosTMP.text = availablePhotos.ToString();
        maxPhotosTMP.text = maxPhotos.ToString();
    } 

    public void TakePhoto()
    {
        if (!viewingPhoto)
        {
            if (canTakePhoto)
            {
                EventManager.TakingPhoto?.Invoke();
                availablePhotos--;
                availablePhotosTMP.text = availablePhotos.ToString();

                if (availablePhotos <= 0)
                {
                    canTakePhoto = false;
                    availablePhotosTMP.color = Color.red;
                }
                StartCoroutine(CapturePhoto());
                //TestCapturePhoto();
            }
            else
            {
                cameraAudio.clip = noPhotosClip;
                cameraAudio.Play();
            }

        }
        else
        {
            RemovePhoto();
            EventManager.RemovePhoto?.Invoke();
        }
    }
    

    private IEnumerator CapturePhoto()//reads renderTexture and copies it to local variable
    {
        cameraUI.SetActive(false);//quitar UI tipo REC
        viewingPhoto = true;
        StartCoroutine(CameraFlashEffect());

        yield return new WaitForEndOfFrame(); //asi lo hara despues de renderizar todo        


        Graphics.CopyTexture(anomaliesCamRT, screenCapture);

        ShowPhoto();
    }

    private void TestCapturePhoto()
    {
        cameraUI.SetActive(false);
        viewingPhoto = true;
        StartCoroutine(CameraFlashEffect());

        //esto seria la manera correcta pero funciona haciendo dos fotos, no a la primera
        AsyncGPUReadback.Request(anomaliesCamRT, 0, (AsyncGPUReadbackRequest action) => 
        {
            
            screenCapture = new Texture2D(anomaliesCamRT.width, anomaliesCamRT.height, anomaliesCamRT.graphicsFormat,
                                          UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
            screenCapture.SetPixelData(action.GetData<byte>(), 0);
            screenCapture.Apply();
        });
        ShowPhoto();
    }

    private void ShowPhoto()//turns texture into sprite and puts it in UI
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;
        //guardar photoSprite para el inventario

        photoFrame.SetActive(true);        
        fadingAnimation.Play("PhotoFade");
    }

    private IEnumerator CameraFlashEffect()
    {
        cameraAudio.clip = cameraClickClip;
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);

    }

    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
    }

}
