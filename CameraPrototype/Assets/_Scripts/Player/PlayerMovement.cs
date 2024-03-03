using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float m_speed, m_rotationSpeedX, m_rotationSpeedY;
    private CharacterController m_charController;
    private Vector2 m_movementInputValue, m_playerRotateInputValue;
    private float m_cameraRotateInputVaue;
    [SerializeField] private float m_upDownRange;
    [SerializeField] private Transform m_camera;

    private void Start()
    {
        m_charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    public void SetMovementInputValue(Vector2 inputValue)
    {
        m_movementInputValue = inputValue;
    }

    public void SetCameraRotateInputValue(float cameraInputValue)
    {
        m_cameraRotateInputVaue = cameraInputValue;
    }
    
    public void SetPlayerRotateInputValue(Vector2 playerInputValue)
    {
        m_playerRotateInputValue = playerInputValue;
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(m_movementInputValue.x, 0, m_movementInputValue.y);

        movement.Normalize();
        movement *= m_speed * Time.deltaTime;

        m_charController.Move(movement);
    }
    
    private void Rotation()
    {
        PlayerRotation();
        CameraRotation();
    }

    private void PlayerRotation()
    {
        float playerRotation = m_playerRotateInputValue.x * m_rotationSpeedX * Time.deltaTime;
        transform.Rotate(0, playerRotation, 0);
    }

    private void CameraRotation()
    {
        float cameraRotation = m_playerRotateInputValue.y *m_rotationSpeedY * Time.deltaTime;
        m_camera.transform.Rotate(cameraRotation, 0, 0);
        //m_camera.transform.rotation = Quaternion.Euler(Mathf.Clamp(m_camera.transform.rotation.x, -m_upDownRange, m_upDownRange), m_camera.transform.rotation.y, 0);
    }
}