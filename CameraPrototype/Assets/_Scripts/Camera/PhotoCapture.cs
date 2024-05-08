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
    //[SerializeField] private RenderTexture anomaliesCamRT;
    [SerializeField] private Camera anomaliesCamera;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;
    [SerializeField] private AudioClip cameraClickClip;
    [SerializeField] private AudioClip noPhotosClip;

    [Header("Scripts")]
    [SerializeField] private SavePhoto savePhoto;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool m_tookFirstPhoto;
    public bool canTakePhoto = true;
    public bool hasCameraEquiped;

    private void Start()
    {

        //anomaliesCamRT esta a 1920 x 1080, en futuro buscar que se adapte al tamanio de la pantalla

    }

    public void TakePhoto()
    {
        m_tookFirstPhoto = true;
        if (!viewingPhoto && hasCameraEquiped)
        {
            Debug.Log("foto");
            
            if (canTakePhoto)
            {
                EventManager.OnTakingPhoto?.Invoke();

                StartCoroutine(CapturePhoto());
                //TestCapturePhoto();
            }
            else
            {

                m_tookFirstPhoto = false;
                cameraAudio.clip = noPhotosClip;
                cameraAudio.Play();
            }

        }

        else
        {
            RemovePhoto();
            EventManager.OnRemovePhoto?.Invoke();
            m_tookFirstPhoto = false;
        }
    }


    public bool GetFirstPhotoTaken()
    {
        return m_tookFirstPhoto;
    }

    public void SetHasCameraEquiped(bool mode)
    {
        hasCameraEquiped = mode;
    }
    

    private IEnumerator CapturePhoto()//reads renderTexture and copies it to local variable
    {
        m_tookFirstPhoto = false;
        cameraUI.SetActive(false);//quitar UI tipo REC
        viewingPhoto = true;

        Time.timeScale = 0;

        StartCoroutine(CameraFlashEffect());

        RenderTexture.ReleaseTemporary(anomaliesCamera.targetTexture);

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        renderTexture.Create();

        anomaliesCamera.targetTexture = renderTexture;

        yield return new WaitForEndOfFrame(); //asi lo hara despues de renderizar todo        

        //RenderTexture anomaliesCamRT = anomaliesCamera.targetTexture;
        //RenderTexture anomaliesCamRT = new RenderTexture(Screen.width, Screen.height, 0);
        //anomaliesCamRT.Create();
        //anomaliesCamRT = anomaliesCamera.targetTexture;
        //print(anomaliesCamRT);
        //anomaliesCamera.targetTexture = anomaliesCamRT;
        //print(anomaliesCamRT);

        

        screenCapture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Graphics.CopyTexture(renderTexture, screenCapture);      


        
        //savePhoto.PhotoSave(screenCapture);

        //RenderTexture.ReleaseTemporary(anomaliesCamRT);
        //anomaliesCamera.targetTexture = null;

        ShowPhoto();
    }

    /*private void TestCapturePhoto()
    {
        m_tookFirstPhoto = false;

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

        savePhoto.PhotoSave(screenCapture);

        ShowPhoto();
    }*/

    private void ShowPhoto()//turns texture into sprite and puts it in UI
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;
        //photoDisplayArea.material.mainTexture = screenCapture;

        savePhoto.PhotoSave(photoSprite);

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

        Time.timeScale = 1;
    }

}
