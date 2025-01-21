using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Projectile projectile;
        [field:SerializeField] public Transform MuzzlePoint { get; private set;}
        [SerializeField] private LayerMask m_LayerMask;
        private PlayerController m_Player;
        private Vector3 m_AimDirection;

        private float m_TimeSinceLastShot;
        [SerializeField] private float m_RateOfFire = 0.5f;
        [SerializeField] private bool m_bIsFullAuto;
        
        #region Properties

        public float TimeSinceLastShot => m_TimeSinceLastShot;
        public float RateOfFire => m_RateOfFire;
        public bool IsFullAuto => m_bIsFullAuto;
      
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
            Debug.Log("Projectile shot");
            Projectile bulletInstance = Instantiate(projectile, MuzzlePoint.position, MuzzlePoint.rotation);
            m_TimeSinceLastShot = 0;
        }


    }
}


