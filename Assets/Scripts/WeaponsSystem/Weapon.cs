using System;
using System.Collections.Generic;
using System.Collections;
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
        [SerializeField] private AmmoInventory m_AmmoInventory;
        [SerializeField] private Transform m_muzzlePoint;
        [SerializeField] private float m_fADSTime = 10.0f;
        private Vector3 m_VOriginalPosition;

        [SerializeField] Vector3 m_VADSPosition;

        private PlayerController m_Player;
        private Vector3 m_AimDirection;
        [SerializeField]private ParticleSystem m_MuzzleFlash;

        private float m_TimeSinceLastShot; 
        [SerializeField] private string m_WeaponName;
        [SerializeField] private float m_RateOfFire = 0.5f;
        [SerializeField] private bool m_bHasFireSelect;
        [SerializeField] private bool m_bIsFullAuto;
        [SerializeField, Range(0.01f, 0.05f)] private float m_fAccuracyCoefficient;
        private float m_fDistanceToTarget;
        
        [SerializeField] private WeaponType m_Type;
        
        [Header("Aim Point Smoothing")] [SerializeField]
        private float m_fCircleRadiusBase = 1f;

        [SerializeField] private float m_fCircleAimRadius = 0.25f;
        private float m_fAimSmoothingTime;
        [SerializeField] private float m_fAimSmoothingTimeHip = 0.5f;
        [SerializeField] private float m_fAimSmoothingTimeADS = 0.25f;
        private float m_fCircleRadiusActual;


        private Vector3 m_vCurrentAimPoint;
        private Vector3 m_vTargetAimPoint;
        private float m_fInterpolationProgress = 1f;

        [SerializeField]private bool m_bIsAiming; //exposed for testing
        [SerializeField]private bool m_bIsFiring = false;
        //[SerializeField]private bool m_bSingleFireTriggered;
        private bool m_bFireButtonHeld;
        private bool m_bFireButtonPressed;
        
        [SerializeField] private float m_fAimPointRaycastDistance = 1000.0f;
        [SerializeField] private LayerMask m_aimLayer;
        [SerializeField] private Transform m_aimPoint;
        
        private float m_timeSinceStarted = 0;
        /*[SerializeField] private float m_kickbackForce = 10.0f;
        [SerializeField] private float m_kickbackAmplitude = 0.5f;*/
        private float m_kickbackTimer;
        private int m_CurrentAmmoInMag;
        [SerializeField] private int m_MaxAmmoInMag;
        
        #region Properties

        public Transform MuzzlePoint => m_muzzlePoint;
        
        public float TimeSinceLastShot => m_TimeSinceLastShot;
        public float RateOfFire => m_RateOfFire;
        public bool IsFullAuto => m_bIsFullAuto;

        public int CurrentAmmoInMag => m_CurrentAmmoInMag;

        public AmmoInventory AmmoInventory => m_AmmoInventory;
        
        public ParticleSystem MuzzleFlash => m_MuzzleFlash;
        
        public WeaponType Type => m_Type;

        public string WeaponName => m_WeaponName;
        
        #endregion'

        private void OnEnable()
        {
            HUDManager.Instance.UpdateFireRateImage(m_bIsFullAuto);
        }

        private void Awake()
        {
            m_Player = GetComponentInParent<PlayerController>();
            m_AmmoInventory = GetComponentInParent<AmmoInventory>();
            //m_aimPoint.GetComponent<MeshRenderer>().enabled = false;
        }

        private void Start()
        {
            if (!m_bIsAiming)
            {
                m_VOriginalPosition = transform.localPosition;
            }
            m_CurrentAmmoInMag = m_MaxAmmoInMag;
            m_fAimSmoothingTime = m_fAimSmoothingTimeHip;
            HUDManager.Instance.UpdateFireRateImage(m_bIsFullAuto);
        }
        
        private void Update()
        {
            m_TimeSinceLastShot += Time.deltaTime;
            m_fCircleRadiusActual = m_fCircleRadiusBase + (m_fDistanceToTarget * m_fAccuracyCoefficient);

            SetIsAiming();
            SetIsFiring();
            
            //TODO create a look at position and do the math by hand
            SetAimPoint();
            this.transform.LookAt(m_aimPoint.position);

            RaycastHit hit;
            bool aim = Physics.Raycast(
                m_muzzlePoint.position,
                m_muzzlePoint.position - m_aimPoint.position, 
                out hit, 
                m_fAimPointRaycastDistance, 
                m_aimLayer);
            if (aim)
            {
                m_AimDirection = m_muzzlePoint.position - hit.point;
                Debug.DrawRay(m_muzzlePoint.position, m_AimDirection * m_fAimPointRaycastDistance, Color.green);
            }
            else
            {
                m_AimDirection = m_muzzlePoint.forward;
                Debug.DrawRay(m_muzzlePoint.position, m_AimDirection * m_fAimPointRaycastDistance, Color.green);
            }
            
            SetADS();
            
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
            BallisticProjectile bulletInstance = Instantiate(projectile, m_muzzlePoint.position, m_muzzlePoint.rotation);
            bulletInstance.Fire(m_muzzlePoint.forward);
            //m_MuzzleFlash?.Play();
            m_TimeSinceLastShot = 0;
            m_CurrentAmmoInMag--;
            HUDManager.Instance.UpdateAmmoInMag(m_CurrentAmmoInMag);
        }

        public void ToggleFireMode()
        {
            if (m_bHasFireSelect)
            {
                m_bIsFullAuto = !m_bIsFullAuto;
            }
            HUDManager.Instance.UpdateFireRateImage(m_bIsFullAuto);
        }

        private void SetADS()
        {
            if (m_bIsAiming)
            {
                m_fAimSmoothingTime = m_fAimSmoothingTimeADS;
                transform.localPosition = Vector3.Lerp(transform.localPosition, m_VADSPosition, m_fADSTime * Time.deltaTime);
            }
            else
            {
                m_fAimSmoothingTime = m_fAimSmoothingTimeHip;
                transform.localPosition = Vector3.Lerp(transform.localPosition, m_VOriginalPosition, m_fADSTime * Time.deltaTime);
            }
        }
        
        private void SetIsFiring()
        {
            //check if button pressed and if there is ammo in mag, if not return
            m_bFireButtonHeld = m_Player.PlayerActions.Player.Attack.IsPressed();
            m_bFireButtonPressed = m_Player.PlayerActions.Player.Attack.WasPerformedThisFrame();
            if (m_bFireButtonHeld && m_bIsFullAuto && m_CurrentAmmoInMag > 0)
            {
                m_bIsFiring = true;
            }
            else if (m_bFireButtonPressed && !m_bIsFullAuto && m_CurrentAmmoInMag > 0)
            {
                m_bIsFiring = true;
                //m_bSingleFireTriggered = true;
            }
            else
            {
                m_bIsFiring = false;
                //m_bSingleFireTriggered = false;
            }
            if (m_bIsFiring && m_bIsFullAuto)
            {
                m_timeSinceStarted += Time.deltaTime; //do I even need this?
            }
            else
            {
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
                float randomRadius = UnityEngine.Random.Range(0f, m_fCircleRadiusActual);

                Vector3 randomPoint = center + upDirection * randomRadius;
                randomPoint += rightDirection * UnityEngine.Random.Range(-m_fCircleRadiusActual, m_fCircleRadiusActual);

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
                               ;
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
                m_aimPoint.position = smoothAimPoint;
                /*
                if(!m_bIsFiring)
                {

                }*/
                /*else if(m_bIsFiring && m_CurrentAmmoInMag > 0)
                {
                    if (m_bIsFullAuto)
                    {
                        m_aimPoint.position += m_aimPoint.up * (Time.deltaTime * m_timeSinceStarted);
                    }
                    if(!m_bIsFullAuto && m_bSingleFireTriggered)
                    {
                        m_aimPoint.position += m_aimPoint.up * (Time.deltaTime * m_fSingleFireRecoilAmount);
                    }
                }*/
            }
            else
            {
                // Go back to original direction if no hit detected
                m_aimPoint.position = direction;
            }

            m_fDistanceToTarget = Vector3.Distance(m_muzzlePoint.position, m_aimPoint.position);
        }

        public void Reload()
        {
            if (m_AmmoInventory.GetAmmoCountByType(m_Type) > 0)
            {
                //case 1: current mag is empty
                if (m_CurrentAmmoInMag <= 0 && m_AmmoInventory.GetAmmoCountByType(m_Type) > m_MaxAmmoInMag)
                {
                    m_AmmoInventory.DecreaseAmmoCount(m_MaxAmmoInMag, m_Type);
                    m_CurrentAmmoInMag = m_MaxAmmoInMag;
                }
                //case 2: current mag has less than max ammo in mag
                else if (m_CurrentAmmoInMag < m_MaxAmmoInMag && m_CurrentAmmoInMag + m_AmmoInventory.GetAmmoCountByType(m_Type) > m_MaxAmmoInMag)
                {
                    int amountToDecrease = m_MaxAmmoInMag - m_CurrentAmmoInMag;
                    m_AmmoInventory.DecreaseAmmoCount(amountToDecrease, m_Type);
                    m_CurrentAmmoInMag = m_MaxAmmoInMag;
                }
                //case 3: if current mag and current inventory is less than the current max ammo in mag
                else if (m_AmmoInventory.GetAmmoCountByType(m_Type) + m_CurrentAmmoInMag < m_MaxAmmoInMag)
                {
                    int amountInMag = m_CurrentAmmoInMag + m_AmmoInventory.GetAmmoCountByType(m_Type);
                    int amountToDecrease = m_AmmoInventory.GetAmmoCountByType(m_Type);
                    m_CurrentAmmoInMag = amountInMag;
                    m_AmmoInventory.DecreaseAmmoCount(amountToDecrease, m_Type);
                }
                //case 4: if current mag + current inventory ammo are equal than the max ammo in mag
                else if (m_MaxAmmoInMag == m_CurrentAmmoInMag + m_AmmoInventory.GetAmmoCountByType(m_Type))
                {
                    m_CurrentAmmoInMag = m_MaxAmmoInMag;
                    m_AmmoInventory.DecreaseAmmoCount(m_AmmoInventory.GetAmmoCountByType(m_Type),
                        m_Type); //get everything down to zero
                }
                else
                {
                    return;
                }
                
                //Update HUD
                HUDManager.Instance.UpdateAmmoInMag(m_CurrentAmmoInMag);
                HUDManager.Instance.UpdateMaxAmmo((m_AmmoInventory.GetAmmoCountByType(m_Type)));
            }

        }

        private void Kickback()
        {
            Debug.Log("Kickback triggered");
        }

        private IEnumerator KickbackCoroutine()
        {
            //maybe just be lazy and use a coroutine?
            //I know there's a lot of overhead
            //maybe use an async operation?
            //I have no clue, actually?
            yield return new WaitForSeconds(0.5f);
        }
    }
}


