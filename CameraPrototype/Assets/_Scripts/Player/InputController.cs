using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //Variables
    private PlayerMovement m_playerMovement;

    private void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        m_playerMovement.SetMovementInputValue(context.ReadValue<Vector2>());
    }

    public void OnCameraMovement(InputAction.CallbackContext context)
    {
        
        m_playerMovement.SetCameraRotateInputValue(context.ReadValue<float>());
    }

    public void OnPlayerRotation(InputAction.CallbackContext context)
    {
        Debug.Log("valorInput: " + context.ReadValue<Vector2>());
        m_playerMovement.SetPlayerRotateInputValue(context.ReadValue<Vector2>());
    }
}
