using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float m_upDownRange;
    [SerializeField] private Transform m_tf_camera;
    private CharacterController m_cc;
    private float m_cameraRotateInputVaue;
    private Vector2 m_v2_movementInputValue, m_v2_playerRotateInputValue;
    public bool m_isCanWalk = true;
    public float speed, rotationSpeedX, rotationSpeedY;

        //gravity
    [SerializeField] private float m_gravity = 9.8f;
    [SerializeField] private float m_terminalVelocity = 10;
    [SerializeField] private float m_verticalVelocity;


    #region Setters
        public void SetMovementInputValue(Vector2 inputValue)
        {
            m_v2_movementInputValue = inputValue;
        }
        
        public void SetCameraRotateInputValue(float cameraInputValue)
        {
            m_cameraRotateInputVaue = cameraInputValue;
        }
        
        public void SetPlayerRotateInputValue(Vector2 playerInputValue)
        {
            m_v2_playerRotateInputValue = playerInputValue;
        }
    #endregion
    
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
        m_cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();

        if (!m_isCanWalk) return;
        Movement();
        Rotation();
    }

    #region Event Methods
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
            m_isCanWalk = false;
        }
        
        private void CanWalkTrue()
        {
            m_isCanWalk = true;
        }
    #endregion
    
    //Custom
    private void Movement()
    {
        Vector3 movement = transform.right * m_v2_movementInputValue.x + transform.forward * m_v2_movementInputValue.y;

        movement.Normalize();

        Vector3 upVector = Vector3.up * (m_verticalVelocity * Time.deltaTime);

        m_cc.Move(movement *(speed * Time.deltaTime) + upVector);
    }
    
    private void Rotation()
    {
        PlayerRotation();
        CameraRotation();
    }

    private void PlayerRotation()
    {
        float playerRotation = m_v2_playerRotateInputValue.x * rotationSpeedX * Time.deltaTime;
        transform.Rotate(0, playerRotation, 0);
    }

    private void CameraRotation()
    {
        Vector3 cameraRotation = new Vector3(-m_v2_playerRotateInputValue.y , 0, 0) * (rotationSpeedY * Time.deltaTime);
        m_tf_camera.Rotate(cameraRotation);

        float angleTransformation = (m_tf_camera.localEulerAngles.x > 180)
            ? m_tf_camera.localEulerAngles.x - 360
            : m_tf_camera.localEulerAngles.x;

        cameraRotation = new Vector3(Mathf.Clamp(angleTransformation, -m_upDownRange, m_upDownRange), 0, 0);

        m_tf_camera.localEulerAngles = cameraRotation;
    }

    private void ApplyGravity()
    {
        if (!m_cc.isGrounded)
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