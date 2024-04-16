using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //Variables
    private PlayerMovement m_playerMovement;
    private PlayerBehaviour m_playerBehaviour;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private PhotoCapture m_photoCapture;

    private void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        m_playerMovement.SetMovementInputValue(context.ReadValue<Vector2>());
    }

    public void OnPlayerRotation(InputAction.CallbackContext context)
    {
        m_playerMovement.SetPlayerRotateInputValue(context.ReadValue<Vector2>());
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
        m_uiManager.PauseMenu();
    }

    public void OnTakePhoto(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!m_uiManager.GetIsPauseMenuActive())
            {
                if (m_photoCapture.GetFirstPhotoTaken() == false)
                {
                    m_photoCapture.TakePhoto();
                }
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerBehaviour.Interaction();
        }
    }
}
