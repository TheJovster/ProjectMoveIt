using System.Runtime.CompilerServices;
using UnityEngine;

//first commit

namespace Characters.Player 
{
    public class PlayerController : MonoBehaviour
    {
        #region Input
        private float m_fXInput => m_InputSystemActions.Player.Move.ReadValue<Vector2>().x;
        private float m_fYInput => m_InputSystemActions.Player.Move.ReadValue<Vector2>().y;

        #endregion

        #region Player Movement - On Land
        private Vector3 m_vMoveDirection;
        [SerializeField] private float m_fJumpDelta = 10.0f;
        [SerializeField] private bool m_bIsInvertedYAxis = false;
        [SerializeField] private float m_fRotationSpeed = 30.0f;
        [SerializeField] private float m_fCurrentMoveSpeed;
        [SerializeField] private float m_fWalkSpeed = 10.0f;
        [SerializeField] private float m_fCrouchedSpeed = 2.5f;
        [SerializeField] private float m_fSprintSpeed = 30.0f;
        [SerializeField] private bool m_bIsSprinting;
        [SerializeField, Range(1.0f, 100.0f)] private float m_fJumpHeight = 10.0f;
        private bool m_bIsJumping = false;
        #endregion
        #region Camera
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private float m_fMaxLookUpValue = 60.0f;
        [SerializeField] private float m_fMinLookUpValue = -60.0f;
        private float m_fCurrentLookUpValue;
        private Vector3 m_vOriginalCameraPosition;
        private float m_fOriginalHeight;
        private float m_fHalfHeight;
        [SerializeField] private bool m_bIsCrouching = false;
        [SerializeField] private float m_fCrouchDelta = 60.0f;
        #endregion
        #region Gravity
        [SerializeField] private float m_fGravityValue = -9.81f;
        [SerializeField] private float m_fGroundedGravityValue = -2.0f;
        #endregion


        #region Component Members
        private CharacterController m_cCharacterController;
        private InputSystem_Actions m_InputSystemActions;
        private PlayerCameraShake m_PlayerCameraShake;
        #endregion
        private void OnEnable()
        {
            if(m_InputSystemActions == null) 
            {
                m_InputSystemActions = new InputSystem_Actions();
                m_InputSystemActions.Enable();
            }
        }

        private void OnDisable()
        {
            m_InputSystemActions.Disable();
        }

        private void Awake()
        {
            if(m_mainCamera == null) 
            {
                m_mainCamera = Camera.main;
            }
            m_cCharacterController = GetComponent<CharacterController>();
            m_PlayerCameraShake = GetComponent<PlayerCameraShake>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_fCurrentMoveSpeed = m_fWalkSpeed;
            m_vOriginalCameraPosition = m_mainCamera.transform.position;
            m_fOriginalHeight = m_cCharacterController.height;
            m_fHalfHeight = m_fOriginalHeight * 0.5f;
            m_fCurrentLookUpValue = 0.0f;
        }

        #region Properties

        public float MoveSpeed => m_fWalkSpeed;
        public float JumpHeight => m_fJumpHeight;
        #endregion

        private void Update()
        {
            MovePlayer();
            RotatePlayer();
            LookUp();
            Sprint();
            Crouch();
            Jump();


            if (m_bIsJumping) 
            {
                m_vMoveDirection.y = m_fGroundedGravityValue;
            }
            else 
            {
                m_vMoveDirection.y += m_fGravityValue * Time.deltaTime;
            }

            m_cCharacterController.Move(new Vector3(m_vMoveDirection.x * m_fCurrentMoveSpeed * Time.deltaTime, m_vMoveDirection.y, m_vMoveDirection.z * m_fCurrentMoveSpeed * Time.deltaTime));
        }

        private void FixedUpdate()
        {
            
        }

        private void MovePlayer() 
        {
            Vector3 vMoveForward = transform.forward * m_fYInput;
            Vector3 vMoveRight = transform.right * m_fXInput;
            m_vMoveDirection = vMoveForward + vMoveRight;
            m_vMoveDirection.Normalize();
        }

        private void LookUp() 
        {
            Vector2 vMouseDelta = m_InputSystemActions.Player.Look.ReadValue<Vector2>();

            if(m_bIsInvertedYAxis)
            m_fCurrentLookUpValue += vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;
            else if (!m_bIsInvertedYAxis)
            m_fCurrentLookUpValue -= vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;

            float fClampedLookUpValue = Mathf.Clamp(m_fCurrentLookUpValue, m_fMinLookUpValue, m_fMaxLookUpValue);

            m_mainCamera.transform.localRotation = Quaternion.Euler(fClampedLookUpValue, 0.0f, 0.0f);
            
        }

        private void RotatePlayer() 
        {
            Vector2 vMouseDelta = m_InputSystemActions.Player.Look.ReadValue<Vector2>();
            transform.Rotate(0.0f, vMouseDelta.x * m_fRotationSpeed * Time.deltaTime, 0.0f);
        }

        private void Sprint()
        {
            m_bIsSprinting = m_InputSystemActions.Player.Sprint.IsPressed();
            if (m_bIsSprinting && !m_bIsJumping && !m_bIsCrouching)
            {
                m_fCurrentMoveSpeed = m_fSprintSpeed;
            }
            else if (!m_bIsSprinting && !m_bIsJumping && !m_bIsCrouching) 
            {
                m_fCurrentMoveSpeed = m_fWalkSpeed;
            }
        }

        private void Jump() 
        {
            if (m_InputSystemActions.Player.Jump.WasPressedThisFrame() && m_cCharacterController.isGrounded && !m_bIsCrouching) 
            {
                m_cCharacterController.Move(new Vector3(0.0f, Mathf.MoveTowards(m_vMoveDirection.y, Mathf.Sqrt(-2.0f * m_fJumpHeight * m_fGravityValue * Time.deltaTime), m_fJumpDelta)));
                Debug.Log("Jumping");
            }
        }

        private void Crouch() 
        {
            if (m_InputSystemActions.Player.Crouch.WasPerformedThisFrame()) 
            {
                m_bIsCrouching = !m_bIsCrouching;
                if (m_bIsCrouching)
                {
                    m_cCharacterController.height = Mathf.MoveTowards(m_cCharacterController.height, m_fHalfHeight, m_fCrouchDelta);
                    m_fCurrentMoveSpeed = m_fCrouchedSpeed;
                }
                else if (!m_bIsCrouching)
                {
                    m_cCharacterController.height = Mathf.MoveTowards(m_cCharacterController.height, m_fOriginalHeight, m_fCrouchDelta);
                    m_fCurrentMoveSpeed = m_fWalkSpeed;
                }
            }
        }
    }
}

