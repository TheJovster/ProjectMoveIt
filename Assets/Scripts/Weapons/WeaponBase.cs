using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase m_weaponProjectile;
    public GameObject m_weaponModel;
    public GameObject m_MuzzlePosition;

    public int Damage;
    public int Range;
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
    public virtual void Load() { }
    public virtual void MeleeAttack() { }
   
}
