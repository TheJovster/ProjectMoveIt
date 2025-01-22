using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace WeaponSystem
{
    public class PlayerController : MonoBehaviour
    {
        private InputSystem_Actions m_InputActions;
        private CharacterController m_CharacterController;
        private Camera m_Camera;
        [SerializeField] private bool m_bIsInvertedYAxis = false;
        [Header("Aiming Values")] [SerializeField]
        private float m_fMinLookUpValue = -60.0f;

        [SerializeField] private float m_fMaxLookUpValue = 60.0f;
        [SerializeField] private float m_fRotationSpeed = 30.0f;
        [SerializeField] private Transform m_weapon;
        private Vector3 m_weaponOriginalPos;




        [SerializeField]
        private Vector3 m_weaponAimPos =>
            new Vector3(0.0f, m_weaponOriginalPos.y, m_weaponOriginalPos.z); //serialized for test purposes


        private float m_fCurrentLookUpValue = 0.0f;



        #region Properties

        public Camera Camera => m_Camera;

        public InputSystem_Actions PlayerActions => m_InputActions;
        
        [field: SerializeField] public Weapon EquippedWeapon { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        
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

            if (m_CharacterController == null)
            {
                m_CharacterController = GetComponent<CharacterController>();
            }
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_weaponOriginalPos = m_weapon.localPosition;
        }

        void Update()
        {

            Move();
            LookUp();
            RotatePlayer();
            
            Fire();
            SwitchFireMode();
            ReloadWeapon();
        }



        private void SwitchFireMode()
        {
            if (m_InputActions.Player.ToggleFireMode.WasPerformedThisFrame())
            {
                EquippedWeapon.ToggleFireMode();;
            }
        }

        //custom functions

        private void LookUp()
        {
            //get mouse delta
            Vector2 vMouseDelta = m_InputActions.Player.Look.ReadValue<Vector2>();

            //set the current look up value
            if (m_bIsInvertedYAxis)
                m_fCurrentLookUpValue += vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;
            else if (!m_bIsInvertedYAxis)
                m_fCurrentLookUpValue -= vMouseDelta.y * m_fRotationSpeed * Time.deltaTime;

            //clamp look up value
            float fClampedLookUpValue = Mathf.Clamp(m_fCurrentLookUpValue, m_fMinLookUpValue, m_fMaxLookUpValue);

            //apply to camera
            m_Camera.transform.localRotation = Quaternion.Euler(fClampedLookUpValue, 0.0f, 0.0f);
        }

        private void RotatePlayer()
        {
            //get mouse delta
            Vector2 vMouseDelta = m_InputActions.Player.Look.ReadValue<Vector2>();
            //apply to transform
            transform.Rotate(0.0f, vMouseDelta.x * m_fRotationSpeed * Time.deltaTime, 0.0f);
        }


        private void Fire()
        {
            //rudimantary - TOOD: Expand the functinality
            if (m_InputActions.Player.Attack.WasPerformedThisFrame() && 
                !EquippedWeapon.IsFullAuto &&
                EquippedWeapon.CurrentAmmoInMag > 0)
            {
                if (EquippedWeapon.TimeSinceLastShot >= EquippedWeapon.RateOfFire)
                {
                    EquippedWeapon.Fire();
                }

            }
            else if (m_InputActions.Player.Attack.IsPressed() && 
                     EquippedWeapon.IsFullAuto &&
                     EquippedWeapon.CurrentAmmoInMag > 0)
            {
                if (EquippedWeapon.TimeSinceLastShot >= EquippedWeapon.RateOfFire)
                {
                    EquippedWeapon.Fire();
                }
            }
        }

        private void ReloadWeapon()
        {
            if (m_InputActions.Player.Reload.WasPerformedThisFrame())
            {
                EquippedWeapon.Reload();
            }
        }

        private void Move()
        {
            //Get Forward and Right Directions
            Vector3 moveForward = transform.forward * m_InputActions.Player.Move.ReadValue<Vector2>().y;
            Vector3 moveRight = transform.right * m_InputActions.Player.Move.ReadValue<Vector2>().x;

            //Add Forward and Right Vectors together and Normalize
            Vector3 moveDirection = moveForward + moveRight;
            moveDirection.Normalize();

            //Apply to Movement
            m_CharacterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
        }

        



    // Visalization
        /*private void OnDrawGizmosSelected()
        {
            RaycastHit outHit;
            Vector3 direction = m_Camera.transform.forward;

            bool rayCast = Physics.Raycast(
                m_Camera.transform.position,
                direction,
                out outHit,
                1000.0f,
                m_aimLayer
            );

            if (rayCast)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(outHit.point, m_fCircleRadius);
            }
        }*/
        
    }
}

