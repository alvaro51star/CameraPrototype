using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
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
    [SerializeField] private CameraManager cameraManager;

    [Header("Input solutions")]
    public bool canTakePhoto = true;
    [FormerlySerializedAs("hasCameraEquiped")] public bool hasCameraEquipped; 
    private bool _viewingPhoto;
    private bool _tookFirstPhoto; 
    
    private Texture2D _screenCapture;
    private List<RenderTexture> _renderTextures;
    private Coroutine _autoRemovePhoto;//needs to be in a var for StopCoroutine to work
    
    
    private void Start()
    {
        _renderTextures = new List<RenderTexture>(cameraManager.GetPhotoCameras().Count);
        
        //create render textures for all photo lenses (cameras). Must be done on Start to work properly.
        foreach (var cameraComp in cameraManager.GetPhotoCameras())
        {
            _renderTextures.Add(new RenderTexture(Screen.width, Screen.height, cameraComp.targetTexture.depth));
            _renderTextures[cameraManager.GetPhotoCameras().IndexOf(cameraComp)].Create();
            cameraComp.targetTexture = _renderTextures[cameraManager.GetPhotoCameras().IndexOf(cameraComp)];
        }
        
        _screenCapture = new Texture2D(_renderTextures[0].width, _renderTextures[0].height, _renderTextures[0].graphicsFormat,
            UnityEngine.Experimental.Rendering.TextureCreationFlags.None);//can stay like this if all cameras have same depth
    }
    
    public void TakePhoto()//called by Input
    {
        _tookFirstPhoto = true;
        if (!_viewingPhoto && hasCameraEquipped)
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


        StartCoroutine(SaveRenderTextureInTexture(_renderTextures[cameraManager.GetActiveCameraIndex()]));
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

    /// <summary>
    /// Removes photo from UI view and returns to game
    /// </summary>
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

    public void SetHasCameraEquipped(bool mode)
    {
        hasCameraEquipped = mode;
    }  

    public bool GetViewingPhoto()
    {
        return _viewingPhoto;
    }
    
}
