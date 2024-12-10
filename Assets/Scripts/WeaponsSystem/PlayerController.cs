using System;
using UnityEditor;
using UnityEngine;

namespace WeaponSystem
{
    public class PlayerController : MonoBehaviour
    {
        private InputSystem_Actions m_InputActions;
        private Camera m_Camera;
        [SerializeField] private bool m_bIsInvertedYAxis = false;
        [SerializeField] private float m_fRotationSpeed = 30.0f;
        private float m_fCurrentLookUpValue = 0.0f;
        [SerializeField] private float m_fMinLookUpValue = -60.0f;
        [SerializeField] private float m_fMaxLookUpValue = 60.0f;
        [SerializeField] private LayerMask m_aimLayer;
        
        #region Properties
        [field:SerializeField] public Transform AimPoint { get; private set; }
        [field:SerializeField] public Weapon EquippedWeapon { get; private set; }

        
        #endregion Properties
        
        private void OnEnable()
        {
            if (m_InputActions == null)
            {
                m_InputActions = new InputSystem_Actions();
                m_InputActions.Enable();
            }
        }

        private void OnDisable()
        {
            m_InputActions.Disable();
        }

        private void Awake()
        {
            if (m_Camera == null)
            {
                m_Camera = GetComponentInChildren<Camera>();
            }

        }
        
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            LookUp();
            RotatePlayer();

            SetAimPoint();
            Fire();
            
            
        }
        
        //custom functions
        
        private void LookUp() 
        {
            Vector2 vMouseDelta = m_InputActions.Player.Look.ReadValue<Vector2>();

            if(m_bIsInvertedYAxis)
                m_fCurrentLookUpValue += vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;
            else if (!m_bIsInvertedYAxis)
                m_fCurrentLookUpValue -= vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;

            float fClampedLookUpValue = Mathf.Clamp(m_fCurrentLookUpValue, m_fMinLookUpValue, m_fMaxLookUpValue);

            m_Camera.transform.localRotation = Quaternion.Euler(fClampedLookUpValue, 0.0f, 0.0f);
        }
        
        private void RotatePlayer() 
        {
            Vector2 vMouseDelta = m_InputActions.Player.Look.ReadValue<Vector2>();
            transform.Rotate(0.0f, vMouseDelta.x * m_fRotationSpeed * Time.deltaTime, 0.0f);
        }

        private void SetAimPoint()
        {
            RaycastHit outHit;
            Vector3 direction = m_Camera.transform.forward; 

            bool rayCast = Physics.Raycast(
                m_Camera.transform.position,
                direction,
                out outHit,
                1000.0f, //TODO expose variable
                m_aimLayer
            );

            AimPoint.position = rayCast ? outHit.point : direction;
            
            Debug.DrawRay(m_Camera.transform.position, direction, Color.red);
        }

        private void Fire()
        {
            if (m_InputActions.Player.Attack.WasPerformedThisFrame())
            {
                EquippedWeapon.Shoot();
            }
        }
    }
}

