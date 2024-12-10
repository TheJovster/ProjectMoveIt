using System;
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
            
        }

        void Update()
        {
            LookUp();
            RotatePlayer();
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
    }
}

