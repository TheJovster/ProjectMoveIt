using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickupBase : MonoBehaviour
{
    public enum PickupType
    {
        HealthPickup,
        WeaponPickup,
        AmmoPickup,
        SpecialPickup
    }

    [SerializeField] protected PickupType Type;
    private SphereCollider m_SphereCollider;
    protected virtual void Awake()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
        m_SphereCollider.isTrigger = true;
    }
}

