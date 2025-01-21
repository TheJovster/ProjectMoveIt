using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeaponSystem
{
    public class PlayerController : MonoBehaviour
    {
        private InputSystem_Actions m_InputActions;
        private CharacterController m_CharacterController;
        private Camera m_Camera;
        [SerializeField] private bool m_bIsInvertedYAxis = false;
        
        [Header("Aiming and Look Values")]
        [SerializeField] private float m_fMinLookUpValue = -60.0f;
        [SerializeField] private float m_fMaxLookUpValue = 60.0f;
        [SerializeField] private LayerMask m_aimLayer;
        [SerializeField] private float m_fRotationSpeed = 30.0f;
        [SerializeField] private float m_fAimPointRaycastDistance = 1000.0f;
        [SerializeField] private Transform m_weapon;
        private Vector3 m_weaponOriginalPos;
        [SerializeField]private Vector3 m_weaponAimPos => new Vector3(0.0f, m_weaponOriginalPos.y, m_weaponOriginalPos.z); //serialized for test purposes
        private bool m_bIsAiming;
        private float m_fCurrentLookUpValue = 0.0f;
        
        
        
        #region Properties
        [field:SerializeField] public Transform AimPoint { get; private set; }
        [field:SerializeField] public Weapon EquippedWeapon { get; private set; }
        [field:SerializeField] public float MoveSpeed { get; private set; }
        
        #endregion Properties
        
        [Header("Aim Point Smoothing")]
        [SerializeField] private float m_fCircleRadius = 1f;
        [SerializeField] private float m_fCircleAimRadius = 0.25f;
        [SerializeField] private float m_fAimSmoothingTime = 0.5f;

        private Vector3 m_vCurrentAimPoint;
        private Vector3 m_vTargetAimPoint;
        private float m_fInterpolationProgress = 1f;

        
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
            m_bIsAiming = m_InputActions.Player.Aim.IsPressed();
            Move();
            LookUp();
            RotatePlayer();

            SetAimPoint();
            Fire();
            
            Debug.Log(m_bIsAiming);
        }
        
        //custom functions
        
        private void LookUp() 
        {
            //get mouse delta
            Vector2 vMouseDelta = m_InputActions.Player.Look.ReadValue<Vector2>();
            
            //set the current look up value
            if(m_bIsInvertedYAxis)
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

        private void SetAimPoint()
        {
            //Raycast
            RaycastHit outHit;
            Vector3 direction = m_Camera.transform.forward; 

            bool rayCast = Physics.Raycast
            (
                m_Camera.transform.position,
                direction,
                out outHit,
                m_fAimPointRaycastDistance, 
                m_aimLayer
            );

            // If raycast hits a valid target
            if (rayCast)
            {
                Vector3 targetCenter = outHit.point;
                
                // Periodically generate a new random aim point within the circle
                if (m_fInterpolationProgress >= 1f)
                {
                    m_vCurrentAimPoint = m_vTargetAimPoint;
                    m_vTargetAimPoint = GenerateRandomCirclePoint(targetCenter, outHit.normal);
                    m_fInterpolationProgress = 0f;
                }

                // Smoothly interpolate between current and target aim points
                m_fInterpolationProgress += Time.deltaTime / m_fAimSmoothingTime;
                Vector3 smoothAimPoint = Vector3.Lerp(m_vCurrentAimPoint, m_vTargetAimPoint, m_fInterpolationProgress);

                AimPoint.position = smoothAimPoint;
            }
            else
            {
                // Go back to original direction if no hit detected
                AimPoint.position = direction;
            }
        }

        private void Fire()
        {
            //rudimantary - TOOD: Expand the functinality
            if (m_InputActions.Player.Attack.WasPerformedThisFrame() && !EquippedWeapon.IsFullAuto)
            {
                if (EquippedWeapon.TimeSinceLastShot >= EquippedWeapon.RateOfFire)
                {
                    EquippedWeapon.Fire();
                }

            }
            else if (m_InputActions.Player.Attack.IsPressed() && EquippedWeapon.IsFullAuto)
            {
                if (EquippedWeapon.TimeSinceLastShot >= EquippedWeapon.RateOfFire)
                {
                    EquippedWeapon.Fire();
                }
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
        
        private Vector3 GenerateRandomCirclePoint(Vector3 center, Vector3 normal)
        {
            // Generate a random point within the circle using polar coordinates
            //m_bIsAiming determines the circle radius
            if (!m_bIsAiming)
            {
                float angle = Random.Range(0f, 2f * Mathf.PI);

                //normal tangent
                Vector3 tangent = new Vector3();
                
                //crossproduct
                Vector3 t1 = Vector3.Cross(normal, Vector3.forward);
                Vector3 t2 = Vector3.Cross(normal, Vector3.up);
                if (t1.magnitude > t2.magnitude)
                {
                    tangent = t1;
                }
                else
                {
                    tangent = t2;
                }
                //normals
                Vector3 upDirection = tangent;
                Vector3 rightDirection = Vector3.Cross(normal, upDirection);
                float randomRadius = Random.Range(0f, m_fCircleRadius);
                
                Vector3 randomPoint = center + upDirection * randomRadius; 
                randomPoint += rightDirection * Random.Range(-m_fCircleRadius, m_fCircleRadius);
                
                // Maintain the same depth as the center point
                //return new Vector3(x, y, center.z);
                return randomPoint;
            }
            else if (m_bIsAiming)
            {
                float angle = Random.Range(0f, 2f * Mathf.PI);

                //normal tangent
                Vector3 tangent = new Vector3();
                //crossproducts
                Vector3 t1 = Vector3.Cross(normal, Vector3.forward);
                Vector3 t2 = Vector3.Cross(normal, Vector3.up);
                if (t1.magnitude > t2.magnitude)
                {
                    tangent = t1;
                }
                else
                {
                    tangent = t2;
                }
                //normals
                Vector3 upDirection = tangent;
                Vector3 rightDirection = Vector3.Cross(normal, upDirection);
                float randomRadius = Random.Range(-m_fCircleAimRadius, m_fCircleAimRadius);
                Vector3 randomPoint = center + upDirection * randomRadius;
                randomPoint += rightDirection * Random.Range(-m_fCircleAimRadius, m_fCircleAimRadius);
                
                // Maintain the same depth as the center point
                return randomPoint;
            }
            else throw new Exception("No aim point");
            // Is there a better way to do this?
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

