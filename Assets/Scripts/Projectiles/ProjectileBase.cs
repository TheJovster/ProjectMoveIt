using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float m_fVelocity = 30.0f; //30.0f by default. The speed the projectile travels with
    [SerializeField] protected int m_Damage;
    protected Vector3 m_MoveDirection;
    protected Weapon m_Owner;
    protected GameObject m_SpawnPoint;
    protected Rigidbody m_RigidBody;
    protected SphereCollider m_SphereCollider;
    protected float m_LifeTime = 5.0f;
    [SerializeField]protected float m_LifeTimeCount = 0.0f;

    protected virtual void OnEnable() 
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_SphereCollider = GetComponent<SphereCollider>();
    }

    protected virtual void Awake()
    {
        m_Owner = GetComponentInParent<Weapon>();
    }


    public virtual void SetOwner(Weapon owner)
    {
        m_Owner = owner;
    }


    public virtual void OnCollisionEnter(Collision other) { }

    
}
