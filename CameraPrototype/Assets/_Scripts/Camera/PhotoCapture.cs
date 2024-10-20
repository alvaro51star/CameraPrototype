using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image m_img_photoDisplayArea;
    [SerializeField] private GameObject m_GO_photoFrame;
    [SerializeField] private GameObject m_GO_cameraUI;
    [SerializeField] private float m_timeShowingPhoto;

    [Header("Flash Effect")]
    [SerializeField] private GameObject m_GO_cameraFlash;
    [SerializeField] private float m_flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator m_animtr_fadingAnimation;

    [Header("Scripts")]
    [SerializeField] private CameraManager m_cameraManager;

    [Header("Input solutions")]
    public bool isAbleToTakePhoto = true;
    public bool isCameraEquipped; 
    private bool m_isViewingPhoto;
    private bool m_isFirstPhotoTaken; 
    
    private Texture2D m_Tex2D_screenCapture;
    private List<RenderTexture> m_rtext_camerasRText;
    private Coroutine m_crt_autoRemovePhoto;//needs to be in a var for StopCoroutine to work

    #region Getters&Setters

    public bool GetFirstPhotoTaken()
    {
        return m_isFirstPhotoTaken;
    }

    public void SetHasCameraEquipped(bool mode)
    {
        isCameraEquipped = mode;
    }  

    public bool GetViewingPhoto()
    {
        return m_isViewingPhoto;
    }

    #endregion

    
    private void Start()
    {
        m_rtext_camerasRText = new List<RenderTexture>(m_cameraManager.GetPhotoCameras().Count);
        
        //create render textures for all photo lenses (cameras). Must be done on Start to work properly.
        foreach (var cameraComp in m_cameraManager.GetPhotoCameras())
        {
            m_rtext_camerasRText.Add(new RenderTexture(Screen.width, Screen.height, cameraComp.targetTexture.depth));
            m_rtext_camerasRText[m_cameraManager.GetPhotoCameras().IndexOf(cameraComp)].Create();
            cameraComp.targetTexture = m_rtext_camerasRText[m_cameraManager.GetPhotoCameras().IndexOf(cameraComp)];
        }
        
        m_Tex2D_screenCapture = new Texture2D(m_rtext_camerasRText[0].width, m_rtext_camerasRText[0].height, m_rtext_camerasRText[0].graphicsFormat,
            UnityEngine.Experimental.Rendering.TextureCreationFlags.None);//can stay like this if all cameras have same depth
    }
    
    
    public void TakePhoto()//called by Input
    {
        m_isFirstPhotoTaken = true;
        if (!m_isViewingPhoto && isCameraEquipped)
        {            
            if (isAbleToTakePhoto)
            {
                StartCoroutine(CameraFlashEffect());
                CapturePhoto();

            }
            else
            {
                m_isFirstPhotoTaken = false;
                AudioManager.Instance.PlayOneShot(FMODEvents.instance.noPhotosClip /*, this.transform.position */);
            }
        }

        else
        {
            StopCoroutine(m_crt_autoRemovePhoto);

            RemovePhoto();  
        }
    }

    private void CapturePhoto()//needs to create the textures in start
    {
        m_isFirstPhotoTaken = false;

        m_GO_cameraUI.SetActive(false);
        m_isViewingPhoto = true;


        StartCoroutine(SaveRenderTextureInTexture(m_rtext_camerasRText[m_cameraManager.GetActiveCameraIndex()]));
    }

    private IEnumerator SaveRenderTextureInTexture(RenderTexture renderTexture)
    {
        yield return new WaitForEndOfFrame();//so flash is rendered
        AsyncGPUReadback.Request(renderTexture, 0, (AsyncGPUReadbackRequest action) =>
        {
            m_Tex2D_screenCapture.SetPixelData(action.GetData<byte>(), 0);//sets the raw data of an entire mipmap level directly in CPU memory
            m_Tex2D_screenCapture.Apply();

            Time.timeScale = 0f;
            EventManager.OnTakingPhoto?.Invoke();
            ShowPhoto();
        });             
    }


    /// <summary>
    /// Turns the photo's texture into a sprite, adds it to the album and shows it in the UI
    /// </summary>
    private void ShowPhoto()
    {
        //save texture in sprite
        UIManager.instance.SetPointersActive(false);
        Sprite photoSprite = Sprite.Create(m_Tex2D_screenCapture, new Rect(0, 0, m_Tex2D_screenCapture.width, m_Tex2D_screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        m_img_photoDisplayArea.sprite = photoSprite;

        AlbumManager.instance.AddPhoto(photoSprite);

        //UI
        m_GO_photoFrame.SetActive(true);        
        m_animtr_fadingAnimation.Play("PhotoFade");
        m_crt_autoRemovePhoto = StartCoroutine(AutoRemovePhoto());

    }

    private IEnumerator CameraFlashEffect()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.cameraClickClip /*, this.transform.position */);

        m_GO_cameraFlash.SetActive(true);
        yield return new WaitForSeconds(m_flashTime);
        m_GO_cameraFlash.SetActive(false);
    }

    /// <summary>
    /// Removes photo from UI view and returns to game
    /// </summary>
    private void RemovePhoto()
    {
        m_isViewingPhoto = false;
        m_GO_photoFrame.SetActive(false);

        EventManager.OnRemovePhoto?.Invoke();
        m_isFirstPhotoTaken = false;
        Time.timeScale = 1f;

        UIManager.instance.SetPointersActive(true);
    }

    private IEnumerator AutoRemovePhoto()
    {
        yield return new WaitForSecondsRealtime(m_timeShowingPhoto);
        if (m_isViewingPhoto)
        {
            RemovePhoto();
        }
    }
    
    
}
