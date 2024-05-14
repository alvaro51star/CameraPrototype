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
    private bool m_isPauseMenuActive = false;
    private bool m_canPause = true;
    [SerializeField] private GameObject endMenu;
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
            m_isPauseMenuActive = false;
            SetPointersActive(true);
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
            m_isPauseMenuActive = true;
            playerPhotoCapture.enabled = false;
            Time.timeScale = 0f;

            if (mouseLimited)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            
        }
    }

    public bool GetIsPauseMenuActive()
    {
        return m_isPauseMenuActive;
    }

    //end menu

    public void ActivateEndMenu()
    {
        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        m_canPause = false;
        m_isPauseMenuActive = true;
        m_cameraUI.SetActive(false);
        endMenu.SetActive(true);
        SetPointersActive(false);
        playerPhotoCapture.enabled = false;
        Time.timeScale = 0f;
    }

    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene(0);//funcionara solo si esta en la scene 0
    }

    //notes
    public void ActivateNote(string noteText)
    {
        m_noteText.text = noteText;
        m_notePanel.SetActive(true);
        SetPointersActive(false);
        EventManager.OnIsReading?.Invoke();
        m_isReading = true;

        m_isPauseMenuActive = true;
    }
    public void DeactivateNote()
    {
        m_noteText.text = " ";
        m_notePanel.SetActive(false);
        EventManager.OnStopReading?.Invoke();
        SetPointersActive(true);
        m_isReading = false;

        m_isPauseMenuActive = false;
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
        }
        else
        {
            EventManager.OnIsReading?.Invoke();
            SetPointersActive(false);
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
