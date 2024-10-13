using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class MenuButtons : MonoBehaviour//en este script estaran las funcionalidades de los botones basicos de los menus
                                        //NO ESTA ACABADO, SIGUE SIENDO UN CAOS
{
    public static MenuButtons instance;
    
    [FormerlySerializedAs("playerPhotoCapture")] [SerializeField] private PhotoCapture m_playerPhotoCapture;
    
    [FormerlySerializedAs("mouseLimited")] [Header("Testing:")]
    public bool iMouseLimited;
    
    [Header("UI Game objects:")]
    [Header ("Menus")]
    [FormerlySerializedAs("pauseMenu")] [SerializeField] private GameObject m_go_pauseMenu;
    [FormerlySerializedAs("optionsMenu")] [SerializeField] private GameObject m_go_optionsMenu;
    [FormerlySerializedAs("loseMenu")] [SerializeField] private GameObject m_go_loseMenu;
    [FormerlySerializedAs("winMenu")] [SerializeField] private GameObject m_go_winMenu;
    [FormerlySerializedAs("audioOptionsMenu")] [SerializeField] private GameObject m_go_audioOptionsMenu;
    [FormerlySerializedAs("m_controls")] [SerializeField] private GameObject m_go_controls;
    
    [Header("Brightness")] public Slider slider;
    public float sliderValue;
    public Image brightnessPanel;
    
    [Header("Temporary here")]
    [FormerlySerializedAs("m_cameraUI")] [SerializeField] private GameObject m_go_cameraUI;//en futuro en vez de desactivarse con este script debería haber un evento
    [FormerlySerializedAs("_diary")] [SerializeField] private GameObject m_go_diary;
    
    private bool m_isGamePaused;

    #region Getters&Setters

    public void SetGameMode(GameModes gameMode)
    {
        if (gameMode == GameModes.InGame)
        {
            Time.timeScale = 1f;
            m_isGamePaused = false;
            
            m_playerPhotoCapture.enabled = true;//para reactivar el input (a futuro se quita esto)
            //poner mapa de controles de en juego

            if (iMouseLimited)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Time.timeScale = 0f;
            m_isGamePaused = true;
            
            m_playerPhotoCapture.enabled = false;//para desactivar el input (a futuro se quita esto)
            //poner mapa de controles de menus
            
            if(!iMouseLimited)
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
            //_isGamePaused = false;
            UIManager.instance.SetPointersActive(true);
            EventManager.OnStopReading?.Invoke();
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
        }
        
        EventManager.OnNotUsingCamera?.Invoke();
        
        SetGameMode(GameModes.InGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivatePauseMenu()
    {
        m_go_pauseMenu.SetActive(true);
        
        SetGameMode(GameModes.UI);
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
            m_go_controls.SetActive(true);
            m_go_pauseMenu.SetActive(false);
        }
        else
        {
            m_go_pauseMenu.SetActive(true);
            m_go_controls.SetActive(false);
        }
    }
    
    public void DiaryButton(bool activate)
    {
        if (activate)
        {
            m_go_diary.SetActive(true);
            m_go_pauseMenu.SetActive(false);
        }
        else
        {
            m_go_pauseMenu.SetActive(true);
            m_go_diary.SetActive(false);
        }
    }
    

    #endregion

    #region Settings

    public void ActivateOptionsMenu()
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

    #endregion

   
}