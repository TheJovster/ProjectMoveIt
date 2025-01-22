using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        
        [SerializeField] private Projectile projectile;
        [field:SerializeField] public Transform MuzzlePoint { get; private set;}
        [SerializeField] private LayerMask m_LayerMask;
        private PlayerController m_Player;
        private Vector3 m_AimDirection;
        [SerializeField]private ParticleSystem m_MuzzleFlash;

        private float m_TimeSinceLastShot;
        [SerializeField] private float m_RateOfFire = 0.5f;
        [SerializeField] private bool m_bIsFullAuto;
        
        [SerializeField] private WeaponType m_Type;
        
        #region Properties

        public float TimeSinceLastShot => m_TimeSinceLastShot;
        public float RateOfFire => m_RateOfFire;
        public bool IsFullAuto => m_bIsFullAuto;

        public ParticleSystem MuzzleFlash => m_MuzzleFlash;
        
        public WeaponType Type => m_Type;
      
        #endregion'

        private void Awake()
        {
            m_Player = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
            m_TimeSinceLastShot += Time.deltaTime;
            
            //TODO create a look at position and do the math by hand
            this.transform.LookAt(m_Player.AimPoint.position);

            RaycastHit hit;
            bool aim = Physics.Raycast(
                MuzzlePoint.position,
                MuzzlePoint.position - m_Player.AimPoint.position, 
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

        public void SetActive()
        {
            this.GameObject().SetActive(true);
        }


        public void Fire()
        {
            //instantiate projectile
            Debug.Log("Projectile shot");
            Projectile bulletInstance = Instantiate(projectile, MuzzlePoint.position, MuzzlePoint.rotation);
            m_MuzzleFlash?.Play();
            m_TimeSinceLastShot = 0;

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

    }
}


