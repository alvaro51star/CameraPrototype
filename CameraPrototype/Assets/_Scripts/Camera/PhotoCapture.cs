using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Cameras")]
    [SerializeField] private Camera anomaliesCamera;
    [SerializeField] private Camera pastCamera;
    
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

    [Header("Scripts")]
    [SerializeField] private SavePhoto savePhoto;

    private Texture2D _screenCapture;
    private bool _viewingPhoto;
    private bool _tookFirstPhoto; //solucion a que el input no vaya muy bien
    public bool canTakePhoto = true;
    public bool hasCameraEquiped; //solucion input

    private RenderTexture _anomalyRenderTexture;
    private RenderTexture _pastRenderTexture;
    private Coroutine _autoRemovePhoto;//needs to be in a var for StopCoroutine to work
    
    
    private void Start()
    {
        if (anomaliesCamera)
        {
            _anomalyRenderTexture = new RenderTexture(Screen.width, Screen.height, anomaliesCamera.targetTexture.depth);
        
            _anomalyRenderTexture.Create(); //needs to be done on start for CapturePhoto() to work properly

            anomaliesCamera.targetTexture = _anomalyRenderTexture;
        }

        if (pastCamera)
        {
            _pastRenderTexture = new RenderTexture(Screen.width, Screen.height, pastCamera.targetTexture.depth);
        
            _pastRenderTexture.Create(); //needs to be done on start for CapturePhoto() to work properly

            pastCamera.targetTexture = _pastRenderTexture;
        }
        _screenCapture = new Texture2D(_anomalyRenderTexture.width, _anomalyRenderTexture.height, _anomalyRenderTexture.graphicsFormat,
            UnityEngine.Experimental.Rendering.TextureCreationFlags.None);//can stay like this if both cameras have same depth
    }
    
    public void TakePhoto()//called by Input
    {
        _tookFirstPhoto = true;
        if (!_viewingPhoto && hasCameraEquiped)
        {            
            if (canTakePhoto)
            {
                StartCoroutine(CameraFlashEffect());
                CapturePhoto();

            }
            else
            {
                _tookFirstPhoto = false;
                AudioManager.Instance.PlayOneShot(FMODEvents.instance.noPhotosClip /*, this.transform.position */);
            }
        }

        else
        {
            StopCoroutine(_autoRemovePhoto);

            RemovePhoto();  
        }
    }

    private void CapturePhoto()//needs to create the textures in start
    {
        _tookFirstPhoto = false;

        cameraUI.SetActive(false);
        _viewingPhoto = true;

        if (anomaliesCamera.isActiveAndEnabled)
        {
            StartCoroutine(SaveRenderTextureInTexture(_anomalyRenderTexture)); 
            //Debug.Log("Foto de " + anomaliesCamera);
        }

        if(!pastCamera)
            return;
        if (pastCamera.isActiveAndEnabled)
        {
            StartCoroutine(SaveRenderTextureInTexture(_pastRenderTexture));     
            //Debug.Log("Foto de " + pastCamera);
        }
    }

    private IEnumerator SaveRenderTextureInTexture(RenderTexture renderTexture)
    {
        yield return new WaitForEndOfFrame();//so flash is rendered
        AsyncGPUReadback.Request(renderTexture, 0, (AsyncGPUReadbackRequest action) =>
        {
            _screenCapture.SetPixelData(action.GetData<byte>(), 0);//sets the raw data of an entire mipmap level directly in CPU memory
            _screenCapture.Apply();

            Time.timeScale = 0f;
            EventManager.OnTakingPhoto?.Invoke();
            ShowPhoto();
        });             
    }


    /// <summary>
    /// Turns the photo's texture into a sprite, saves it and shows it in the UI
    /// </summary>
    private void ShowPhoto()
    {
        //save texture in sprite
        UIManager.instance.SetPointersActive(false);
        Sprite photoSprite = Sprite.Create(_screenCapture, new Rect(0, 0, _screenCapture.width, _screenCapture.height),
                                           new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;

        AlbumManager.instance.AddPhoto(photoSprite);

        //UI
        photoFrame.SetActive(true);        
        fadingAnimation.Play("PhotoFade");
        _autoRemovePhoto = StartCoroutine(AutoRemovePhoto());

    }

    private IEnumerator CameraFlashEffect()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.cameraClickClip /*, this.transform.position */);

        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    private void RemovePhoto()
    {
        _viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);

        EventManager.OnRemovePhoto?.Invoke();
        _tookFirstPhoto = false;
        Time.timeScale = 1f;

        UIManager.instance.SetPointersActive(true);
    }

    private IEnumerator AutoRemovePhoto()
    {
        yield return new WaitForSecondsRealtime(timeShowingPhoto);
        if (_viewingPhoto)
        {
            RemovePhoto();
        }
    }
    
    public bool GetFirstPhotoTaken()
    {
        return _tookFirstPhoto;
    }

    public void SetHasCameraEquiped(bool mode)
    {
        hasCameraEquiped = mode;
    }  

    public bool GetViewingPhoto()
    {
        return _viewingPhoto;
    }
    
}
