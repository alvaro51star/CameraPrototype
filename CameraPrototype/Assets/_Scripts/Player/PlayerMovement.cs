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

    private void OnEnable()
    {
        EventManager.TakingPhoto += CanWalkFalse;
        EventManager.RemovePhoto += CanWalkTrue;
        EventManager.IsReading += IsReading;
        EventManager.StopReading += StopReading;
    }

    private void OnDisable()
    {
        EventManager.TakingPhoto -= CanWalkFalse;
        EventManager.RemovePhoto -= CanWalkTrue;
        EventManager.IsReading   -= IsReading;
        EventManager.StopReading -= StopReading;
    }

    private void Start()
    {
        m_charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (m_canWalk)
        {
            Movement();
            Rotation();
        }
    }

    public void IsReading()
    {
        CanWalkFalse();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StopReading()
    {
        CanWalkTrue();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CanWalkFalse()
    {
        m_canWalk = false;
    }

    public void CanWalkTrue()
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
        //Vector3 movement = new Vector3(m_movementInputValue.x, 0, m_movementInputValue.y);
        Vector3 movement = transform.right * m_movementInputValue.x + transform.forward * m_movementInputValue.y;

        movement.Normalize();

        m_charController.Move(movement *(m_speed * Time.deltaTime));
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

        float angleTransfomation = (m_camera.localEulerAngles.x > 180)
            ? m_camera.localEulerAngles.x - 360
            : m_camera.localEulerAngles.x;

        cameraRotation = new Vector3(Mathf.Clamp(angleTransfomation, -m_upDownRange, m_upDownRange), 0, 0);

        m_camera.localEulerAngles = cameraRotation;
    }
}