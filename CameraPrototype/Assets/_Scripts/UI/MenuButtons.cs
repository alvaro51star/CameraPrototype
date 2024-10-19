using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour//en este script estaran las funcionalidades de los botones basicos de los menus
                                        //NO ESTA ACABADO, SIGUE SIENDO UN CAOS
{
    public static MenuButtons instance;
    
    [Header("Testing:")]
    public bool isMouseLimited;
    
    [Header("UI Game objects:")]
    [Header ("Menus")]
    [SerializeField] private GameObject m_go_pauseMenu;
    [SerializeField] private GameObject m_go_optionsMenu;
    [SerializeField] private GameObject m_go_loseMenu;
    [SerializeField] private GameObject m_go_winMenu;
    [SerializeField] private GameObject m_go_audioOptionsMenu;
    [SerializeField] private GameObject m_go_controls;
    
    [Header("Brightness")] public Slider slider;
    public float sliderValue;
    public Image brightnessPanel;
    
    [Header("Mouse sensibility")]
    public Slider sliderSensibilidadX;
    public Slider sliderSensibilidadY;
    public float SensValueX;
    public float SensValueY;
    public PlayerMovement m_playerMovement;
    
    [Header("Temporary here")]
    [SerializeField] private GameObject m_go_cameraUI;//en futuro en vez de desactivarse con este script debería haber un evento
    [SerializeField] private PhotoCapture m_playerPhotoCapture;
    
    private bool m_isGamePaused;

    #region Getters&Setters

    /// <summary>
    /// Changes timeScale and mouse limitation depending on the game mode
    /// </summary>
    /// <param name="gameMode"></param>
    public void SetGameMode(GameModes gameMode)
    {
        if (gameMode == GameModes.InGame)
        {
            Time.timeScale = 1f;
            m_isGamePaused = false;
            
            m_playerPhotoCapture.enabled = true;//para reactivar el input (a futuro se quita esto)
            //poner mapa de controles de en juego

            if (isMouseLimited)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else //GameModes.UI
        {
            Time.timeScale = 0f;
            m_isGamePaused = true;
            
            m_playerPhotoCapture.enabled = false;//para desactivar el input (a futuro se quita esto)
            //poner mapa de controles de menus
            
            if(!isMouseLimited)
                return;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public bool GetIsGamePaused()
    {
        return m_isGamePaused;
    }

    public void SetIsGamePaused(bool mode)
    {
        m_isGamePaused = mode;
    }
    public bool GetIsPauseMenuActive()
    {
        return m_go_pauseMenu.activeSelf;
    }

    public void SetPauseMenu(bool active)
    {
        m_go_pauseMenu.SetActive(active);
    }

    #endregion
    

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
            SetGameMode(GameModes.InGame);
        else
            SetGameMode(GameModes.UI);
        
        
        if(!slider)//para que no de error en las escenas de prueba que no lo tienen (arreglo provisional)
            return;

        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);

    }

    #region Basic buttons
    
    public void Resume()
    {
        m_go_cameraUI.SetActive(true);
        m_go_pauseMenu.SetActive(false);
        UIManager.instance.ShowAlbum(false);
        
        if (!UIManager.instance.m_isReading)
        {
            m_isGamePaused = false;
            UIManager.instance.SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
        }
        
        SetGameMode(GameModes.InGame);
        EventManager.OnNotUsingCamera?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void LoadMainMenuScene()
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
            m_go_controls.SetActive(true);
            m_go_pauseMenu.SetActive(false);
        }
        else
        {
            m_go_pauseMenu.SetActive(true);
            m_go_controls.SetActive(false);
        }
    }
    public void OptionsMenuButton()
    {
        if (!m_go_optionsMenu.activeSelf)
        {
            m_go_optionsMenu.SetActive(true);
            m_go_pauseMenu.SetActive(false);
        }
        else
        {
            m_go_optionsMenu.SetActive(false);
            m_go_pauseMenu.SetActive(true);
        }
    }

    #endregion

    #region Settings

    public void AudioOptionsMenu()
    {
        if (!m_go_audioOptionsMenu.activeSelf)
        {
            m_go_audioOptionsMenu.SetActive(true);
            m_go_optionsMenu.SetActive(false);
        }
        else
        {
            m_go_audioOptionsMenu.SetActive(false);
            m_go_optionsMenu.SetActive(true);
        }
    }
    
    //Brightness
    public void BrightnessSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);
    }
    
    //Mouse sensibility
    public void SensibilitySliderX(float X)
    {
        SensValueX = X;
        m_playerMovement.m_rotationSpeedX = SensValueX;
    }

    public void SensibilitySliderY(float Y)
    {
        SensValueY = Y;
        m_playerMovement.m_rotationSpeedY = SensValueY;
    }

    #endregion

   
}