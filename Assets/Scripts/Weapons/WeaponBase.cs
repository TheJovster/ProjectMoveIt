using UnityEngine;
using UnityEngine.Serialization;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase m_weaponProjectile;
    public GameObject m_weaponModel;
    public GameObject m_MuzzlePosition;

    [SerializeField] protected int m_iDamage;
    [SerializeField] protected int m_iRange;
    [SerializeField] protected bool m_bIsFullAuto;

    #region Properties

    public bool IsFullAuto => m_bIsFullAuto;
    public int Damage => m_iDamage;
    public int Range => m_iRange;

    #endregion Properties
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    //does Rarity even make sense? Maybe?

    public enum Type 
    {
        Pistol,
        SMG,
        Shotgun,
        AssaultRifle,
        LMG,
        Sniper,
        DMR
    }

    public virtual void Equip() { }
    public virtual void Fire() { }
    public virtual void Reload() { }
    public virtual void MeleeAttack() { }
   
}
