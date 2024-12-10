using System;
using UnityEngine;

namespace WeaponSystem
{
    public class Projectile : MonoBehaviour
    {

        private Rigidbody m_rigidBody;
        private float m_lifeTime = 0.0f;
        [SerializeField] private float m_projectileSpeed = 30.0f; //30 by default. Recommended value much higher.
        
        
        private void Awake()
        {
            m_rigidBody = GetComponent<Rigidbody>();
            
        }

        private void Start()
        {
            m_rigidBody.AddForce(transform.forward * m_projectileSpeed, ForceMode.Impulse);
        }

        private void Update()
        {
            SelfDestruct();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("PlayerWeapon")) return;
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void SelfDestruct()
        {

            m_lifeTime += Time.deltaTime;
            if (m_lifeTime >= 3.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

