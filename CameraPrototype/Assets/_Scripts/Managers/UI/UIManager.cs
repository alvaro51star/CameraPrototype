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
        //Notes
    [SerializeField] private GameObject m_notePanel;
    [SerializeField] private TextMeshProUGUI m_noteText;
    //Input
    [SerializeField] private GameObject m_interactInputImage;

    public GameObject dialoguePanel;

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
        m_isPauseMenuActive = false;
        m_cameraUI.SetActive(true);
        pauseMenu.SetActive(false);
        playerPhotoCapture.enabled = true;

        if (mouseLimited)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        ShowInput(false);
    }
    public void DeactivateNote()
    {
        m_noteText.text = " ";
        m_notePanel.SetActive(false);
        EventManager.OnStopReading?.Invoke();
        ShowInput(true);
    }

    //Input
    public void ShowInput(bool mode)
    {
        m_interactInputImage.SetActive(mode);
    }
}
