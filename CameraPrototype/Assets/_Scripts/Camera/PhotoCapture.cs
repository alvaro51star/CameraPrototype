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
    [SerializeField] private float timeShowingPhoto;

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

    private Sprite m_photoSprite;

    private void Start()
    {
        m_renderTexture = new RenderTexture(Screen.width, Screen.height, anomaliesCamera.targetTexture.depth);
        m_renderTexture.Create(); //creando la textura al principio ha empezado a funcionar bien CapturePhoto()

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
                StartCoroutine(CameraFlashEffect());
                CapturePhoto();

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

    private void CapturePhoto()//needs to create the textures in start
    {
        m_tookFirstPhoto = false;

        cameraUI.SetActive(false);
        viewingPhoto = true;

        StartCoroutine(SaveRenderTextureInTexture());        
    }

    private IEnumerator SaveRenderTextureInTexture()
    {
        yield return new WaitForEndOfFrame();//para que flash este renderizado
        AsyncGPUReadback.Request(m_renderTexture, 0, (AsyncGPUReadbackRequest action) =>
        {
            screenCapture.SetPixelData(action.GetData<byte>(), 0);//sets the raw data of an entire mipmap level directly in CPU memory
            screenCapture.Apply();

            Time.timeScale = 0f;
            EventManager.OnTakingPhoto?.Invoke();
            ShowPhoto();
        });             
    }


    private void ShowPhoto()//turns texture into sprite, puts it in UI and saves it calling PhotoSave
    {

        UIManager.instance.SetPointersActive(false);
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;
        m_photoSprite = photoSprite;

        //savePhoto.PhotoSave(screenCapture);

        AlbumManager.instance.AddPhoto(photoSprite);

        photoFrame.SetActive(true);        
        fadingAnimation.Play("PhotoFade");
        StartCoroutine(AutoRemovePhoto());
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

        EventManager.OnRemovePhoto?.Invoke();
        m_tookFirstPhoto = false;
        Time.timeScale = 1f;

        UIManager.instance.SetPointersActive(true);

    }

    private IEnumerator AutoRemovePhoto()
    {
        yield return new WaitForSecondsRealtime(timeShowingPhoto);
        if (viewingPhoto)
        {
            RemovePhoto();
        }
    }

    public bool GetViewingPhoto()
    {
        return viewingPhoto;
    }
}
