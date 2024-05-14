using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;

    [SerializeField] private PhotoCapture playerPhotoCapture;
    [Header("Testing:")]
    [SerializeField] private bool mouseLimited;

    [Header("UI Gameobjects:")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject diaryPanel;
    private bool m_isGamePaused = false;
    private bool m_canPause = true;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject m_cameraUI;
    [SerializeField] private GameObject m_controls;
    //Notes
    [SerializeField] private GameObject m_notePanel;
    [SerializeField] private TextMeshProUGUI m_noteText;
    private bool m_isReading;
    //Interaction
    [SerializeField] private GameObject m_interactInputImage;
    [SerializeField] private GameObject m_punteroInteraction;
    [SerializeField] private GameObject m_punteros;
    [SerializeField] private TextMeshProUGUI m_interactionText;
    [SerializeField] private GameObject m_lockImage;
    private bool m_isLockedDoor;
    //Dialogue
    public GameObject dialoguePanel;
    //puzles
    //CajaFuerte
    [SerializeField] private GameObject m_safePanel;
    [SerializeField] private GameObject m_redLight;
    [SerializeField] private GameObject m_greenLight;
    [SerializeField] private TextMeshProUGUI m_safeNumberText;

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
        if(mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //pause menu
    public void Resume()
    {
        m_cameraUI.SetActive(true);
        pauseMenu.SetActive(false);
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
        EventManager.OnNotUsingCamera?.Invoke();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PauseMenu()
    {
        if (m_canPause)
        {
            pauseMenu.SetActive(true);
            m_cameraUI.SetActive(false);
            SetPointersActive(false);
            SetInteractionText(false, "");
            m_isGamePaused = true;
            playerPhotoCapture.enabled = false;
            Time.timeScale = 0f;

            if (mouseLimited)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }            
        }
    }

    public void Diary()
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
    }

    public bool GetIsGamePaused()
    {
        return m_isGamePaused;
    }

    public bool GetIsPauseMenuActive()
    {
        return pauseMenu.activeSelf;
    }

    public bool GetIsReading()
    {
        return m_isReading;
    }

    //end menus

    public void ActivateLoseMenu()
    {
        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        m_canPause = false;
        m_isGamePaused = true;
        m_cameraUI.SetActive(false);
        loseMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        playerPhotoCapture.enabled = false;
        Time.timeScale = 0f;
    }

    

    public void ActivateWinMenu()
    {
        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        m_canPause = false;
        m_isGamePaused = true;
        m_cameraUI.SetActive(false);
        winMenu.SetActive(true);
        SetPointersActive(false);
        SetInteractionText(false, "");
        playerPhotoCapture.enabled = false;
        Time.timeScale = 0f;
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

        m_isGamePaused = true;
    }
    public void DeactivateNote()
    {
        m_noteText.text = " ";
        m_notePanel.SetActive(false);
        EventManager.OnStopReading?.Invoke();
        SetPointersActive(true);
        m_isReading = false;

        m_isGamePaused = false;
    }

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

    public void InteractionAvialable(bool mode, bool isLockedDoor)
    {
        if (isLockedDoor)
        {
            ChangeDoorLock(true);
            SetInteractionText(false, "");
        }
        else
        {
            ChangeDoorLock(false);
            ChangeInteractionPointer(mode);
        }
    }

    public void SetPointersActive(bool mode)
    {
        m_punteros.SetActive(mode);
        m_lockImage.SetActive(m_isLockedDoor);
        if (!mode)
        {
            ShowInput(false);
        }
    }

    public void Controls()
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
    }

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

            m_isGamePaused = false;
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
            SetPointersActive(false);
            m_isReading = true;

            m_isGamePaused = true;
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
}
