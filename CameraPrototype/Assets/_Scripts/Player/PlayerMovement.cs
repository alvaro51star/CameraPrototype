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
    public bool m_canWalk = true;

    //gravity
    [SerializeField] private float m_gravity = 9.8f;
    [SerializeField] private float m_terminalVelocity = 10;
    [SerializeField] private float m_verticalVelocity;


    private void OnEnable()
    {
        EventManager.OnTakingPhoto += CanWalkFalse;
        EventManager.OnRemovePhoto += CanWalkTrue;
        EventManager.OnIsReading += IsReading;
        EventManager.OnStopReading += StopReading;
    }

    private void OnDisable()
    {
        EventManager.OnTakingPhoto -= CanWalkFalse;
        EventManager.OnRemovePhoto -= CanWalkTrue;
        EventManager.OnIsReading   -= IsReading;
        EventManager.OnStopReading -= StopReading;
    }

    private void Start()
    {
        m_charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();

        if (!m_canWalk) return;
        Movement();
        Rotation();
    }

    private void IsReading()
    {
        CanWalkFalse();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void StopReading()
    {
        CanWalkTrue();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CanWalkFalse()
    {
        m_canWalk = false;
    }

    private void CanWalkTrue()
    {
        m_canWalk = true;
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
        Vector3 movement = transform.right * m_movementInputValue.x + transform.forward * m_movementInputValue.y;

        movement.Normalize();

        //boorrar luego
        Vector3 upVector = Vector3.up * (m_verticalVelocity * Time.deltaTime);
        print(upVector);

        m_charController.Move(movement *(m_speed * Time.deltaTime) + upVector);
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
        Vector3 cameraRotation = new Vector3(-m_playerRotateInputValue.y , 0, 0) * (m_rotationSpeedY * Time.deltaTime);
        m_camera.Rotate(cameraRotation);

        float angleTransformation = (m_camera.localEulerAngles.x > 180)
            ? m_camera.localEulerAngles.x - 360
            : m_camera.localEulerAngles.x;

        cameraRotation = new Vector3(Mathf.Clamp(angleTransformation, -m_upDownRange, m_upDownRange), 0, 0);

        m_camera.localEulerAngles = cameraRotation;
    }

    private void ApplyGravity()
    {
        if (!m_charController.isGrounded)
        {
            if (m_verticalVelocity <= -m_terminalVelocity)
            {
                m_verticalVelocity = -m_terminalVelocity;
            }
            else
            {
                m_verticalVelocity -= m_gravity * Time.deltaTime;
            }
        }
        else
        {
            m_verticalVelocity = 0;
        }
    }
}