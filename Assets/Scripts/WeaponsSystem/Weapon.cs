using System;
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

        private void Awake()
        {
            m_Player = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
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
                m_AimDirection = MuzzlePoint.position - hit.normal;
                Debug.DrawRay(MuzzlePoint.position, m_AimDirection, Color.green);
            }
            else
            {
                m_AimDirection = MuzzlePoint.forward;
                Debug.DrawRay(MuzzlePoint.position, m_AimDirection, Color.green);
            }

        }

        public void Shoot()
        {
            Debug.Log("Projectile shot");
           
            Projectile bulletInstance = Instantiate(projectile, m_AimDirection, MuzzlePoint.rotation);
        }
    }
}


