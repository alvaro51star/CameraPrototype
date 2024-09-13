using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;

    //[SerializeField] private PhotoCapture playerPhotoCapture;
    //[Header("Testing:")]
    //[SerializeField] private bool mouseLimited;

    [Header("UI Gameobjects:")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject diaryPanel;
    [SerializeField] private GameObject storyBookPanel;
    //[SerializeField] private GameObject optionsMenu;

    //private bool m_isGamePaused = false;
    private bool m_canPause = true;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject m_cameraUI;
    [SerializeField] private GameObject m_controls;

    //Audio
    [SerializeField] private GameObject audioOptionsMenu;

    //Notes
    [SerializeField] private GameObject m_notePanel;
    [SerializeField] private TextMeshProUGUI m_noteText;
    public bool m_isReading;
    //Interaction
    [SerializeField] private GameObject m_interactInputImage;
    [SerializeField] private GameObject m_punteroInteraction;
    [SerializeField] private GameObject m_punteros;
    [SerializeField] private TextMeshProUGUI m_interactionText;
    [SerializeField] private GameObject m_lockImage;
    [SerializeField] private GameObject m_petImage;
    private bool m_isLockedDoor;
    private bool m_isPetCat;
    //Dialogue
    public GameObject dialoguePanel;
    public bool m_isInDialogue;
    //puzles
    //CajaFuerte
    [SerializeField] private GameObject m_safePanel;
    [SerializeField] private GameObject m_redLight;
    [SerializeField] private GameObject m_greenLight;
    [SerializeField] private TextMeshProUGUI m_safeNumberText;


    //Album
    [SerializeField] private GameObject m_albumPanel;
    [SerializeField] private GameObject m_CloseUpImagePanel;
    [SerializeField] private GameObject m_prevAlbumButtom;
    [SerializeField] private GameObject m_nextAlbumButtom;
    [SerializeField] private GameObject[] m_albumPhotos;
    [SerializeField] private Image[] m_albumPhotosSprites;
    [SerializeField] private Image m_closeUpPhoto;
    private int m_albumPage = -1; //empieza en -1


    //Brillo
    public Slider slider;
    public float sliderValue;
    public Image BrightnessPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /*private void Start()
    {
        if(mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        BrightnessPanel.color = new Color(BrightnessPanel.color.r, BrightnessPanel.color.g, BrightnessPanel.color.b, sliderValue);

    }*/

    #region OldPauseMenu

    //no borro estas funciones todavia para no dar errores y por si acaso se me olvida algo
    
    public void Resume()//no puedo borrar esta funcion porque sino tendria que tocar el input controller
        //y todavia no he acabado la rama de la lente del pasado
    {
        /*m_cameraUI.SetActive(true);
        pauseMenu.SetActive(false);
        ShowAlbum(false);
        playerPhotoCapture.enabled = true;

        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (!m_isReading)
        {
            m_isGamePaused = false;
            SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
        }

        Time.timeScale = 1f;
        EventManager.OnNotUsingCamera?.Invoke();*/
        MenuButtons.instance.Resume();
    }
    /*public void Exit()
    {
        Application.Quit();
    }*/
    public void PauseMenu()
    {
        if (m_canPause)
        {
            //pauseMenu.SetActive(true);
            m_cameraUI.SetActive(false);
            SetPointersActive(false);
            SetInteractionText(false, "");
            //m_isGamePaused = true;
            //playerPhotoCapture.enabled = false;
            
            MenuButtons.instance.ActivatePauseMenu();
        }
    }
    
    /*public void Controls() //no era necesario, el juego ya esta pausado en el menu de pausa
                             //y para desactivar/activar cosas mejor hacerlas por el editor
{
    m_controls.SetActive(true);
    m_cameraUI.SetActive(false);
    playerPhotoCapture.enabled = false;
    pauseMenu.SetActive(false);

    if (mouseLimited)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}*/
    /*public void LoadSceneMainMenu()
{
    SceneManager.LoadScene(0);//funcionara solo si esta en la scene 0
}

public void Restart()
{
    SceneManager.LoadScene(1);
}*/

    /*public void OptionsMenu()
{
    if (!optionsMenu.activeSelf)
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    else
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}

public void AudioOptionsMenu()
{
    if (!audioOptionsMenu.activeSelf)
    {
        audioOptionsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    else
    {
        audioOptionsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}*/
    /*public void Diary()//al igual que los controles, creo que es mejor hacer esto por editor para no tener tantas funciones
    {
        if(!diaryPanel.activeSelf)
        {
            diaryPanel.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            diaryPanel.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }*/
    #endregion
    
    

    public void StoryBook()
    {
        if (!storyBookPanel.activeSelf)
        {
            storyBookPanel.SetActive(true);
            pauseMenu.SetActive(false);

            SetPointersActive(false);
            EventManager.OnIsReading?.Invoke();
            m_isReading = true;
            SetInteractionText(false, "");
        }
        else
        {
            storyBookPanel.SetActive(false);

            SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
            m_isReading = false;
        }
    }



    public bool GetIsGamePaused()//no borro esta funcion de aqui para no generar cambios en InputController
    {
        //return m_isGamePaused;
        return MenuButtons.instance.GetIsGamePaused();
    }

    /*public void SetIsGamePaused(bool mode)//no borro esta funcion de aqui para no generar cambios en InputController
    {
        m_isGamePaused = mode;
    }*/

    public bool GetIsPauseMenuActive()
    {
        return pauseMenu.activeSelf;
    }

    public bool GetIsReading()
    {
        return m_isReading;
    }

    public void SetIsReading(bool mode)
    {
        m_isReading = mode;
    }

    //end menus

    public void ActivateLoseMenu()
    {

        m_canPause = false;
        //m_isGamePaused = true;
        m_cameraUI.SetActive(false);
        loseMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        /*playerPhotoCapture.enabled = false;
        Time.timeScale = 0f;
        
        if(!MenuButtons.instance.mouseLimited)
            return;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/
        MenuButtons.instance.GameMode(GameModes.UI);
    }

    

    public void ActivateWinMenu()
    {
        MenuButtons.instance.GameMode(GameModes.UI);
        
        /*if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        m_canPause = false;
        m_isGamePaused = true;*/
        m_cameraUI.SetActive(false);
        winMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        //playerPhotoCapture.enabled = false;
        //Time.timeScale = 0f;
    }

    //notes
    public void ActivateNote(string noteText)
    {
        m_noteText.text = noteText;
        m_notePanel.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        EventManager.OnIsReading?.Invoke();
        m_isReading = true;

        //m_isGamePaused = true; //no entiendo esto, entonces al abrir una nota no se para el tiempo y tal??
        MenuButtons.instance.SetIsGamePaused(true);
    }
    public void DeactivateNote()
    {
        m_noteText.text = " ";
        m_notePanel.SetActive(false);
        EventManager.OnStopReading?.Invoke();
        SetPointersActive(true);
        m_isReading = false;

        //m_isGamePaused = false;
        MenuButtons.instance.SetIsGamePaused(false);
    }

    #region InteractiveObjectsHUD
    //Interaction
    public void ShowInput(bool mode)
    {
        m_interactInputImage.SetActive(mode);
    }

    public void ChangeInteractionPointer(bool mode)
    {
        if (mode)
        {
            m_punteroInteraction.SetActive(true);
            ShowInput(true);
        }
        else
        {
            m_punteroInteraction.SetActive(false);
            ShowInput(false);
        }
    }

    public void SetInteractionText(bool mode, string text)
    {
        if (mode)
        {
            m_interactionText.gameObject.SetActive(true);
            m_interactionText.text = text;
        }
        else
        {
            m_interactionText.text = "";
            m_interactionText.gameObject.SetActive(false);
        }
    }

    public void ChangeDoorLock(bool mode)
    {
        if (m_lockImage.activeSelf != mode)
        {
            m_punteros.SetActive(!mode);
            ShowInput(!mode);
            m_lockImage.SetActive(mode);
            m_isLockedDoor = mode;
        }
    }

    public void ChangePetCat(bool mode)
    {
        if (m_petImage.activeSelf != mode)
        {
            m_punteros.SetActive(!mode);
            ShowInput(!mode);
            m_petImage.SetActive(mode);
            m_isPetCat= mode;
        }
    }

    public void InteractionAvialable(bool mode, bool isLockedDoor, bool isCat)//me da miedo cambiar el nombre por si toco otros scripts
    {
        if (isLockedDoor && !isCat)
        {
            ChangeDoorLock(true);
            ChangePetCat(false);
            SetInteractionText(false, "");
        }
        else if(!isLockedDoor && !isCat)
        {
            ChangeDoorLock(false);
            ChangePetCat(false);
            ChangeInteractionPointer(mode);
        }
        else if (!isLockedDoor && isCat)
        {
            ChangeDoorLock(false);
            ChangePetCat(true);
            SetInteractionText(false, "");
        }
    }

    public void SetPointersActive(bool mode)
    {
        m_punteros.SetActive(mode);
        m_lockImage.SetActive(m_isLockedDoor);
        m_petImage.SetActive(m_isPetCat);
        if (!mode)
        {
            ShowInput(false);
        }
    }
    #endregion
    

    #region InteractiveObjectsUI
    //puzles
    //Caja fuerte
    
    public void ShowSafe(bool mode)
    {
        m_safePanel.SetActive(mode);
        if (!mode)
        {
            EventManager.OnStopReading?.Invoke();
            SetPointersActive(true);
            SetInteractionText(false, "");
            m_isReading = false;

            //m_isGamePaused = false;
            MenuButtons.instance.SetIsGamePaused(false);
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
            SetPointersActive(false);
            m_isReading = true;

            //m_isGamePaused = true;
            MenuButtons.instance.SetIsGamePaused(true);
        }
    }

    public void ShowLight(bool mode, bool redLight)
    {
        if (mode)
        {
            if (redLight)
            {
                m_redLight.SetActive(true);
                m_greenLight.SetActive(false);
            }
            else
            {
                m_redLight.SetActive(false);
                m_greenLight.SetActive(true);
            }
        }
        else
        {
            m_redLight.SetActive(false);
            m_greenLight.SetActive(false);
        }
    }

    public void ChangeCodeDisplay(string num)
    {
        m_safeNumberText.text = num;
    }
    #endregion


    #region Album

    public void ShowAlbum(bool mode)
    {
        if (mode)
        {
            m_albumPanel.SetActive(true);
            AdvanceAlbumPage();
        }
        else
        {
            m_CloseUpImagePanel.SetActive(false);
            m_albumPanel.SetActive(false);
            m_albumPage = -1;
        }
    }

    public void AdvanceAlbumPage()
    {
        m_albumPage++;
        List<Sprite> sprites = AlbumManager.instance.GetTandaPhoto(m_albumPage, m_albumPhotosSprites.Length);
        bool showButtons = false;
        for (int i = 0; i < m_albumPhotos.Length; i++) //cambiar a lista
        {
            if (i < sprites.Count)
            {
                m_albumPhotos[i].SetActive(true);
                m_albumPhotosSprites[i].sprite = sprites[i];
                showButtons = true;
            }
            else
            {
                m_albumPhotos[i].gameObject.SetActive(false);
            }
        }
        if (showButtons == false)
        {
            m_prevAlbumButtom.SetActive(false);
            m_nextAlbumButtom.SetActive(false);
        }
        else
        {
            if (m_albumPage != 0)
            {
                m_prevAlbumButtom.SetActive(true);
            }
            else
            {
                m_prevAlbumButtom.SetActive(false);
            }

            if (m_albumPage * m_albumPhotosSprites.Length + sprites.Count < AlbumManager.instance.GetPhotoCount())
            {
                m_nextAlbumButtom.SetActive(true);
            }
            else
            {
                m_nextAlbumButtom.SetActive(false);
            }
        }
    }

    public void GoBackAlbumPage()
    {
        if (m_albumPage > 0)
        {
            m_albumPage--;
            List<Sprite> sprites = AlbumManager.instance.GetTandaPhoto(m_albumPage, m_albumPhotosSprites.Length);
            bool showButtons = false;
            for (int i = 0; i < m_albumPhotos.Length; i++) //cambiar a lista
            {
                if (i < sprites.Count)
                {
                    m_albumPhotos[i].SetActive(true);
                    m_albumPhotosSprites[i].sprite = sprites[i];
                    showButtons = true;
                }
                else
                {
                    m_albumPhotos[i].gameObject.SetActive(false);
                }
            }
            if (showButtons == false)
            {
                m_prevAlbumButtom.SetActive(false);
                m_nextAlbumButtom.SetActive(false);
            }
            else
            {
                if (m_albumPage != 0)
                {
                    m_prevAlbumButtom.SetActive(true);
                }
                if (m_albumPage * m_albumPhotosSprites.Length + sprites.Count < AlbumManager.instance.GetPhotoCount())
                {
                    m_nextAlbumButtom.SetActive(true);
                }
                else
                {
                    m_nextAlbumButtom.SetActive(false);
                }
            }
        }
        if (m_albumPage == 0)
        {
            m_prevAlbumButtom.SetActive(false);
        }
    }

    public void ShowCloseUpPhoto(Image photo)
    {
        m_CloseUpImagePanel.SetActive(true);
        m_closeUpPhoto.sprite = photo.sprite;
    }

    public void CloseCloseUpPhoto()
    {
        m_CloseUpImagePanel.SetActive(false);
    }


    #endregion
    
    public void BrightnessSlider(float valor)//borrar despues, esta en MenuButtons
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        BrightnessPanel.color = new Color(BrightnessPanel.color.r, BrightnessPanel.color.g, BrightnessPanel.color.b, sliderValue);
    }
}
