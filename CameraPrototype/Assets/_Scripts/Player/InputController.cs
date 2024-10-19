using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //Variables
    [SerializeField] private PhotoCapture m_photoCapture;
    [SerializeField] private DialogueController m_dialogueController;
    private PlayerMovement m_playerMovement;
    private PlayerBehaviour m_playerBehaviour;

    private void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerBehaviour = GetComponent<PlayerBehaviour>();
        m_dialogueController = UIManager.instance.GetComponent<DialogueController>();
    }

    //Custom
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
        if (MenuButtons.instance.GetIsPauseMenuActive())
        {
            MenuButtons.instance.Resume();
        }
        else
        {
            UIManager.instance.PauseMenu();
        }
    }

    public void OnTakePhoto(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if ((MenuButtons.instance.GetIsGamePaused() || !m_photoCapture.isCameraEquipped) &&
            (MenuButtons.instance.GetIsGamePaused() || m_photoCapture.GetViewingPhoto() != true)) return;
        if (m_photoCapture.GetFirstPhotoTaken() == false)
        {
            m_photoCapture.TakePhoto();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        switch (context.control.name)
        {
            case "e":
                m_playerBehaviour.InputInteraction();
                break;
            case "leftButton":
            {
                if (MenuButtons.instance.GetIsGamePaused() is false || !m_photoCapture.isCameraEquipped || m_photoCapture.GetViewingPhoto() != true)
                    m_playerBehaviour.InputInteraction();
                break;
            }
        }
    }

    public void OnFocusCamera(InputAction.CallbackContext context)
    {
        if (context.started && !MenuButtons.instance.GetIsGamePaused())
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
        if (context.performed && UIManager.instance.isInDialogue)
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

    public void OnChangeLens(InputAction.CallbackContext context)//lo dejo por si acaso metemos mas lentes
    {
        if(MenuButtons.instance.GetIsGamePaused())
            return;
        if(context.ReadValue<float>() == 0f)
            return;
        EventManager.OnChangeLens?.Invoke(context.ReadValue<float>());
    }
}
