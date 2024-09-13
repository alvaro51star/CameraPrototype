using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{
    public static MenuButtons instance;
    
    [SerializeField] private PhotoCapture playerPhotoCapture;
    
    [Header("Testing:")]
    public bool mouseLimited;
    
    [Header("UI Game objects:")]
    [SerializeField] private GameObject pauseMenu;
    //[SerializeField] private GameObject diaryPanel;
    //[SerializeField] private GameObject storyBookPanel;
    [SerializeField] private GameObject optionsMenu;

    private bool _isGamePaused;
    //private bool _canPause = true;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject m_cameraUI;
    [SerializeField] private GameObject m_controls;
    [SerializeField] private GameObject _diary;

    //Audio
    [SerializeField] private GameObject audioOptionsMenu;


    [Header("Brightness")] public Slider slider;
    public float sliderValue;
    public Image brightnessPanel;

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
    
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)Scenes.Level1)
            GameMode(GameModes.InGame);
        else
            GameMode(GameModes.UI);
        
        
        if(!slider)//para que no de error en las escenas de prueba que no lo tienen (arreglo provisional)
            return;

        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);

    }

    #region Basic buttons
    
    public void Resume()
    {
        m_cameraUI.SetActive(true);
        pauseMenu.SetActive(false);
        UIManager.instance.ShowAlbum(false);
        
        if (!UIManager.instance.m_isReading)
        {
            //_isGamePaused = false;
            UIManager.instance.SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
        }
        
        EventManager.OnNotUsingCamera?.Invoke();
        
        GameMode(GameModes.InGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        
        GameMode(GameModes.UI);
    }
    
    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);//funcionara solo si las escenas estan ordenadas como el enum
    }

    public void Restart()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }

    public void ControlsButton(bool activate)
    {
        if (activate)
        {
            m_controls.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
            m_controls.SetActive(false);
        }
    }
    
    public void DiaryButton(bool activate)
    {
        if (activate)
        {
            _diary.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
            _diary.SetActive(false);
        }
    }
    

    #endregion

    #region Settings

    public void ActivateOptionsMenu()
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
    }
    
    //Brightness
    public void BrightnessSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);
    }

    #endregion

    public void GameMode(GameModes gameMode)
    {
        if (gameMode == GameModes.InGame)
        {
            Time.timeScale = 1f;
            _isGamePaused = false;
            
            playerPhotoCapture.enabled = true;//para reactivar el input (a futuro se quita esto)
            //poner mapa de controles de en juego

            if (mouseLimited)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Time.timeScale = 0f;
            _isGamePaused = true;
            
            playerPhotoCapture.enabled = false;//para desactivar el input (a futuro se quita esto)
            //poner mapa de controles de menus
            
            if(!mouseLimited)
                return;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public bool GetIsGamePaused()
    {
        return _isGamePaused;
    }

    public void SetIsGamePaused(bool mode)
    {
        _isGamePaused = mode;
    }

}