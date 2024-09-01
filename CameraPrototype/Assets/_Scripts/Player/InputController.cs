using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //Variables
    private PlayerMovement m_playerMovement;
    private PlayerBehaviour m_playerBehaviour;
    [SerializeField] private PhotoCapture m_photoCapture;
    [SerializeField] private DialogueController m_dialogueController;

    private void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerBehaviour = GetComponent<PlayerBehaviour>();
        m_dialogueController = UIManager.instance.GetComponent<DialogueController>();
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
        if (UIManager.instance.GetIsPauseMenuActive())
        {
            UIManager.instance.Resume();
        }
        else
        {
            UIManager.instance.PauseMenu();
        }
    }

    public void OnTakePhoto(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if ((UIManager.instance.GetIsGamePaused() || !m_photoCapture.hasCameraEquiped) &&
            (UIManager.instance.GetIsGamePaused() || m_photoCapture.GetViewingPhoto() != true)) return;
        if (m_photoCapture.GetFirstPhotoTaken() == false)
        {
            m_photoCapture.TakePhoto();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        print("control = " + context.control.name);
        switch (context.control.name)
        {
            case "e":
                m_playerBehaviour.InputInteraction();
                break;
            case "leftButton":
            {
                if (UIManager.instance.GetIsGamePaused() is false || !m_photoCapture.hasCameraEquiped || m_photoCapture.GetViewingPhoto() != true)
                {
                    print("input con ratón" );
                    m_playerBehaviour.InputInteraction();
                    
                }

                break;
            }
        }
    }

    public void OnFocusCamera(InputAction.CallbackContext context)
    {
        if (context.started && !UIManager.instance.GetIsGamePaused())
        {
            EventManager.OnUsingCamera?.Invoke();
        }
        else if (context.canceled)
        {
            EventManager.OnNotUsingCamera?.Invoke();
        }
    }

    public void OnSkipText(InputAction.CallbackContext context)
    {
        if (context.performed && UIManager.instance.m_isInDialogue)
        {
            m_dialogueController.NextDialogueLine();
        }
    }

    public void OnCameraRollCheatCode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.OnAddRoll?.Invoke(100);
        }
    }
}
