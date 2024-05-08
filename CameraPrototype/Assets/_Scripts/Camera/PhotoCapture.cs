using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera anomaliesCamera;

    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;

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
    private bool m_tookFirstPhoto; //solucion a que el input no vaya muy bien
    public bool canTakePhoto = true;
    public bool hasCameraEquiped; //solucion input

    private RenderTexture m_renderTexture;

    private void Start()
    {
        m_renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        m_renderTexture.Create(); //creando la textura al principio ha empezado a funcionar el TestCapturePhoto()

        anomaliesCamera.targetTexture = m_renderTexture;

        screenCapture = new Texture2D(m_renderTexture.width, m_renderTexture.height, m_renderTexture.graphicsFormat,
                              UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
    }
    public void TakePhoto()
    {
        m_tookFirstPhoto = true;
        if (!viewingPhoto && hasCameraEquiped)
        {            
            if (canTakePhoto)
            {
                EventManager.OnTakingPhoto?.Invoke();
                Time.timeScale = 0f;
                StartCoroutine(CameraFlashEffect());
                //StartCoroutine(CapturePhoto());
                TestCapturePhoto();
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
            Time.timeScale = 1f;
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
    
    //NO SE PUEDE GUARDAR LAS FOTOS CON ESTO, EL COPY TEXTURE SOLO LO HACE LA GPU Y NO PASA A LA CPU
    private IEnumerator CapturePhoto()//reads renderTexture and copies it to local Texture2D
    {
        m_tookFirstPhoto = false;
        cameraUI.SetActive(false);//quitar UI tipo REC
        viewingPhoto = true;       

        //RenderTexture.ReleaseTemporary(anomaliesCamera.targetTexture);

        //RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        //renderTexture.Create();

        anomaliesCamera.targetTexture = m_renderTexture;
        screenCapture = new Texture2D(m_renderTexture.width, m_renderTexture.height, TextureFormat.ARGB32, false);

        yield return new WaitForEndOfFrame(); //asi lo hara despues de renderizar todo        

        //RenderTexture anomaliesCamRT = anomaliesCamera.targetTexture;
        //RenderTexture anomaliesCamRT = new RenderTexture(Screen.width, Screen.height, 0);
        //anomaliesCamRT.Create();
        //anomaliesCamRT = anomaliesCamera.targetTexture;
        //print(anomaliesCamRT);
        //anomaliesCamera.targetTexture = anomaliesCamRT;
        //print(anomaliesCamRT);
       
        Graphics.CopyTexture(m_renderTexture, screenCapture);      

        //RenderTexture.ReleaseTemporary(anomaliesCamRT);
        //anomaliesCamera.targetTexture = null;

        ShowPhoto();
    }

    private void TestCapturePhoto()//only worked in second photo until I created the texture in the start??
        //no usamos ReadPixels() porque no funciona en RenderTexture
    {
        m_tookFirstPhoto = false;

        cameraUI.SetActive(false);
        viewingPhoto = true;
       
        
        //RenderTexture.ReleaseTemporary(anomaliesCamera.targetTexture); //esto da error en la segunda foto ????

        //RenderTexture m_renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        //m_renderTexture.Create();

        //anomaliesCamera.targetTexture = m_renderTexture;        

        //screenCapture = new Texture2D(m_renderTexture.width, m_renderTexture.height, m_renderTexture.graphicsFormat,
                              //UnityEngine.Experimental.Rendering.TextureCreationFlags.None);

        //esto seria la manera correcta pero funciona haciendo dos fotos, no a la primera
        AsyncGPUReadback.Request(m_renderTexture, 0, (AsyncGPUReadbackRequest action) => 
        {
            screenCapture.SetPixelData(action.GetData<byte>(), 0);//sets the raw data of an entire mipmap level directly in CPU memory
            screenCapture.Apply();
            //Debug.Log("TestCapturePhoto taking photo");
            ShowPhoto(); //even though it does save an image, it is really dark (looks af if flash isn't working) and the UI works weird 
        });             //parece que esta cogiendo datos no actualizados (de antes del flash)
        
    }


    private void ShowPhoto()//turns texture into sprite, puts it in UI and saves it calling PhotoSave
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;

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
    }

}
