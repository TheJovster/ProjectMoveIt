using System;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponType
        {
            Pistol,
            AssaultRifle,
            SMG,
            DMR,
            Sniper,
            LMG
        }
        
        [SerializeField] private BallisticProjectile projectile;
        [SerializeField]private AmmoInventory m_AmmoInventory;
        [field:SerializeField] public Transform MuzzlePoint { get; private set;}
        [SerializeField] private LayerMask m_LayerMask;
        private PlayerController m_Player;
        private Vector3 m_AimDirection;
        [SerializeField]private ParticleSystem m_MuzzleFlash;

        private float m_TimeSinceLastShot;
        [SerializeField] private float m_RateOfFire = 0.5f;
        [SerializeField] private bool m_bIsFullAuto;
        
        [SerializeField] private WeaponType m_Type;
        
        [Header("Aim Point Smoothing")] [SerializeField]
        private float m_fCircleRadius = 1f;

        [SerializeField] private float m_fCircleAimRadius = 0.25f;
        [SerializeField] private float m_fAimSmoothingTime = 0.5f;


        private Vector3 m_vCurrentAimPoint;
        private Vector3 m_vTargetAimPoint;
        private float m_fInterpolationProgress = 1f;

        [SerializeField]private bool m_bIsAiming; //exposed for testing
        [SerializeField] private bool m_bIsFiring;
        
        [SerializeField] private float m_fAimPointRaycastDistance = 1000.0f;
        [SerializeField] private LayerMask m_aimLayer;
        [SerializeField] private Transform m_aimPoint;
        
        private float m_timeSinceStarted = 0;
        [SerializeField] private Vector3 m_originalPosition;
        [SerializeField] private float m_kickbackForce = 10.0f;
        [SerializeField] private float m_kickbackAmplitude = 0.5f;
        private float m_kickbackTimer;
        private int m_CurrentAmmoInMag;
        [SerializeField] private int m_MaxAmmoInMag;
        
        #region Properties

        public float TimeSinceLastShot => m_TimeSinceLastShot;
        public float RateOfFire => m_RateOfFire;
        public bool IsFullAuto => m_bIsFullAuto;

        public int CurrentAmmoInMag => m_CurrentAmmoInMag;


        public ParticleSystem MuzzleFlash => m_MuzzleFlash;
        
        public WeaponType Type => m_Type;
      
        #endregion'

        private void Awake()
        {
            m_Player = GetComponentInParent<PlayerController>();
            m_AmmoInventory = GetComponentInParent<AmmoInventory>();
        }

        private void Start()
        {
            m_originalPosition = transform.localPosition;
            m_CurrentAmmoInMag = m_MaxAmmoInMag;
        }
        
        private void Update()
        {
            m_TimeSinceLastShot += Time.deltaTime;

            SetIsAiming();
            SetIsFiring();
            
            //TODO create a look at position and do the math by hand
            SetAimPoint();
            this.transform.LookAt(m_aimPoint.position);

            RaycastHit hit;
            bool aim = Physics.Raycast(
                MuzzlePoint.position,
                MuzzlePoint.position - m_aimPoint.position, 
                out hit, 
                1000.0f, 
                m_LayerMask);
            if (aim)
            {
                m_AimDirection = MuzzlePoint.position - hit.point;
                Debug.DrawRay(MuzzlePoint.position, m_AimDirection * 1000.0f, Color.green);
            }
            else
            {
                m_AimDirection = MuzzlePoint.forward;
                Debug.DrawRay(MuzzlePoint.position, m_AimDirection * 1000.0f, Color.green);
            }

        }

        private void SetIsAiming()
        {
            m_bIsAiming = m_Player.PlayerActions.Player.Aim.IsPressed();
        }

        public void SetActive()
        {
            this.GameObject().SetActive(true);
        }


        public void Fire()
        {
            //instantiate projectile
            BallisticProjectile bulletInstance = Instantiate(projectile, MuzzlePoint.position, MuzzlePoint.rotation);
            bulletInstance.Fire(MuzzlePoint.forward);
            m_MuzzleFlash?.Play();
            m_TimeSinceLastShot = 0;
            m_CurrentAmmoInMag--;
            
            //decrement ammo
            switch (m_Type)
            {
                case WeaponType.Pistol:
                    //TOOD decrement pistol ammo
                    break;
                case WeaponType.AssaultRifle:
                    //TODO decrement assault rifle ammo
                    break;
                case WeaponType.SMG:
                    //TODO decrement SMG ammo
                    break;
                case WeaponType.DMR:
                    //TODO Decerement DMR ammo;
                    break;
                case WeaponType.Sniper:
                    //TODO decrement sniper ammo;
                    break;
                case WeaponType.LMG:
                    //TODO decrement LMG ammo;
                    break;
            }
            //do I add pooling system?
        }

        public void ToggleFireMode()
        {
            m_bIsFullAuto = !m_bIsFullAuto;
        }
        
        private void SetIsFiring()
        {
            //TODO check if there is ammo in mag, if not return
            m_bIsFiring = m_Player.PlayerActions.Player.Attack.IsPressed() && m_CurrentAmmoInMag > 0;
            if (m_bIsFiring)
            {
                m_timeSinceStarted += Time.deltaTime; //do I even need this?
            }
            else
            {
                //m_lastSavedTimeSinceStarted = m_timeSinceStarted;
                m_timeSinceStarted = 0;
            }
        }
        
        private Vector3 GenerateRandomCirclePoint(Vector3 center, Vector3 normal)
        {
            // Generate a random point within the circle using polar coordinates
            //m_bIsAiming determines the circle radius
            if (!m_bIsAiming)
            {
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
                float randomRadius = UnityEngine.Random.Range(0f, m_fCircleRadius);

                Vector3 randomPoint = center + upDirection * randomRadius;
                randomPoint += rightDirection * UnityEngine.Random.Range(-m_fCircleRadius, m_fCircleRadius);

                // Maintain the same depth as the center point
                //return new Vector3(x, y, center.z);
                return randomPoint;
            }
            else if (m_bIsAiming)
            {
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
                float randomRadius = UnityEngine.Random.Range(-m_fCircleAimRadius, m_fCircleAimRadius);
                Vector3 randomPoint = center + upDirection * randomRadius;
                randomPoint += rightDirection * UnityEngine.Random.Range(-m_fCircleAimRadius, m_fCircleAimRadius);

                // Maintain the same depth as the center point
                return randomPoint;
            }
            else throw new Exception("No aim point");
            // Is there a better way to do this?
        }
        
        private void SetAimPoint()
        {
            //Raycast
            RaycastHit outHit;
            Vector3 direction = m_Player.Camera.transform.forward;

            bool rayCast = Physics.Raycast
            (
                m_Player.Camera.transform.position,
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
                if(!m_bIsFiring)
                {
                    m_aimPoint.position = smoothAimPoint;
                }
                else if(m_bIsFiring && m_CurrentAmmoInMag > 0)
                {
                    m_aimPoint.position += m_aimPoint.up * (Time.deltaTime * m_timeSinceStarted); //TODO expose the value
                }
            }
            else
            {
                // Go back to original direction if no hit detected
                m_aimPoint.position = direction;
            }
        }

        public void Reload()
        {
            if (m_CurrentAmmoInMag <= 0 && m_AmmoInventory.GetAmmoCountByType(m_Type) > m_MaxAmmoInMag)
            {
                m_AmmoInventory.DecreaseAmmoCount(m_MaxAmmoInMag, m_Type);
                m_CurrentAmmoInMag = m_MaxAmmoInMag;
            }
            else if (m_CurrentAmmoInMag > 0 && m_AmmoInventory.GetAmmoCountByType(m_Type) > 0)
            {
                int amountToSubtract = m_MaxAmmoInMag - m_CurrentAmmoInMag;
                m_AmmoInventory.DecreaseAmmoCount(amountToSubtract, m_Type);
                m_CurrentAmmoInMag = m_MaxAmmoInMag + 1;
            }
            else if (m_AmmoInventory.GetAmmoCountByType(m_Type) < m_MaxAmmoInMag &&
                     m_AmmoInventory.GetAmmoCountByType(m_Type) > 0)
            {
                int amountToSubtract = m_AmmoInventory.GetAmmoCountByType(m_Type);
                m_AmmoInventory.DecreaseAmmoCount(amountToSubtract, m_Type);
                m_CurrentAmmoInMag += amountToSubtract;
                //I need to sketch this
                //too tired
                //need food
            }
            else if((m_AmmoInventory.GetAmmoCountByType(m_Type) + m_CurrentAmmoInMag) < m_MaxAmmoInMag)
            {
                int amount = m_CurrentAmmoInMag + m_AmmoInventory.GetAmmoCountByType(m_Type);
                m_AmmoInventory.DecreaseAmmoCount(m_AmmoInventory.GetAmmoCountByType(m_Type), m_Type);
                m_CurrentAmmoInMag = amount;
            }
            else return;
            //edgecase if the current weapon type ammo is 0

        }

        private void Kickback()
        {
                Debug.Log("Kickback triggered");
        }


    }
}


