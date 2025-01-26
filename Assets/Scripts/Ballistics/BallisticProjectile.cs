using System;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace WeaponSystem
{
    public class BallisticProjectile : MonoBehaviour
{
    [Header("Ballistic Projectile Settings")] [SerializeField]
    private float m_fInitialProjectileVelocity = 30.0f; // I am assuming I will have to make this a lot faster later on

    //gravity and drag
    [Header("Projectile Physics")]
    [SerializeField] private float m_fGravityValue = -9.81f;
    [SerializeField, Range(0.0f, 1.0f)] private float m_fDragCoeficient = 0.1f;

    //velocity and movement variables
    private Vector3 m_vCurrentVelocity;
    private Vector3 m_vStartPosition;
    private bool m_bIsFlying = false; //initialize this at start - set it to true I guess?
    private float m_fTimeInFlight = 0.0f; //initialized at 0, add Time.fixedDeltaTime in FIXED UPDATE

    //ricochet variables
    [Header("Projectile Ricochet Settings")]
    [SerializeField, Range(0, 10)] private int m_iMaxRicochetCount;
    [SerializeField, Range(0.0f, 1.0f)] private float m_fRicochetVelocityLoss = 0.5f;
    [SerializeField] private float m_fMinRicochetAngle = 10.0f;
    [SerializeField] private float m_fMaxRicochetAngle = 60.0f;
    private int m_iRicochetCount = 0;
    
    private void Start()
    {
        m_bIsFlying = true;
    }

    private void FixedUpdate()
    {
        if (m_bIsFlying)
        {
            //add fixed delta time on time in flight
            m_fTimeInFlight += Time.fixedDeltaTime;

            //apply gravity to the projectile
            m_vCurrentVelocity.y += m_fGravityValue * Time.fixedDeltaTime;

            //apply drag (or air resistance)
            float f_currentVelocityMagnitude = m_vCurrentVelocity.magnitude;
            float f_dragForce = -m_fDragCoeficient * m_vCurrentVelocity.magnitude;
            m_vCurrentVelocity += m_vCurrentVelocity.normalized * (f_dragForce * Time.fixedDeltaTime);

            //move the transform position
            transform.position += m_vCurrentVelocity * Time.fixedDeltaTime;

            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        RaycastHit hit;
        Vector3 direction = transform.position - m_vStartPosition;
        float distance = direction.magnitude;

        if (Physics.Raycast(m_vStartPosition, direction.normalized, out hit, distance))
        {
            HandleImpact(hit, direction.normalized);
        }

        m_vStartPosition = transform.position;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void HandleImpact(RaycastHit hit, Vector3 incomingDirection)
    {
        
        //ricochet logic
        if (m_iRicochetCount < m_iMaxRicochetCount)
        {
            float incidentAngle = Vector3.Angle(incomingDirection, hit.normal);

            if (incidentAngle >= m_fMinRicochetAngle && incidentAngle <= m_fMaxRicochetAngle)
            {
                Vector3 reflectedVelocity = Vector3.Reflect(m_vCurrentVelocity, hit.normal);
                m_vCurrentVelocity = reflectedVelocity * (1.0f - m_fRicochetVelocityLoss);

                m_iRicochetCount++;

                transform.position = hit.point + hit.normal * 0.1f;

                return;
            }
        }
        
        //stop movement
        m_bIsFlying = false;
        
        //impact effects should go here - TODO: Implement Interface
        if (hit.transform.CompareTag("Target"))
        {
            hit.transform.gameObject.GetComponent<Target>().SetActive();
            //Destroy(gameObject);
        }
        else if (!hit.transform.CompareTag("Player"))
        {
            //Destroy(gameObject);
        }
        

        //more stuff?
        if (!m_bIsFlying) Destroy(gameObject);
    }

    public void Fire(Vector3 direction)
    {
        direction.Normalize();
        m_vCurrentVelocity = direction * m_fInitialProjectileVelocity;
        m_bIsFlying = true;
        m_fTimeInFlight = 0.0f;
        m_vStartPosition = transform.position;
    }
}
}

