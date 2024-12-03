using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    #region ParentComponents

    private PlayerWeaponInventory m_playerWeaponInventroy;

    #endregion
    
    public GameObject m_weaponModel;
    public GameObject m_MuzzlePosition;

    [SerializeField] private int m_iDamage;
    [SerializeField] private int m_iRange;
    [SerializeField] private bool m_bIsFullAuto;
    [SerializeField] private Type m_eType;
    [SerializeField] private float m_fRateOfFire;
    private float m_fTimeSinceLastShot;
    private bool m_bIsActive;

    #region Properties

    public float TimeSinceLastShot => m_fTimeSinceLastShot;
    public float RateOfFire => m_fRateOfFire;
    public bool IsFullAuto => m_bIsFullAuto;
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

    private void OnEnable()
    {
        m_playerWeaponInventroy = GetComponentInParent<PlayerWeaponInventory>();
    }

    private void Awake()
    {
        if (m_eType == Type.AssaultRifle || m_eType == Type.LMG || m_eType == Type.SMG)
        {
            m_bIsFullAuto = true;
        }
        else if (m_eType == Type.Pistol || m_eType == Type.DMR || m_eType == Type.Sniper || m_eType == Type.Shotgun)
        {
            m_bIsFullAuto = false;
        }
        
    }

    private void Update()
    {
        m_fTimeSinceLastShot += Time.deltaTime;
        if (m_fTimeSinceLastShot >= 100.0f)
        {
            m_fTimeSinceLastShot = 100.0f;
        }
    }

    public void Equip()
    {
        this.m_bIsActive = true;
        this.gameObject.SetActive(true);
    }

    public void Uneqip()
    {
        this.m_bIsActive = false;
        this.gameObject.SetActive(false);

    }

    public void Fire()
    {
        ProjectileBase projectile = null;

        switch (m_eType)
        {
            case Type.Pistol:
                projectile = AmmoObjectPool.Instance.GetPooledPistolAmmo();
                break;
            case Type.Shotgun:
                projectile = AmmoObjectPool.Instance.GetPooledShotgunAmmo();
                break;
            case Type.SMG:
                projectile = AmmoObjectPool.Instance.GetPooledSMGAmmo();
                break;
            case Type.AssaultRifle:
                projectile = AmmoObjectPool.Instance.GetPooledAssaultRifleAmmo();
                break;
            case Type.DMR:
                projectile = AmmoObjectPool.Instance.GetPooledDMRAmmo();
                break;
            case Type.Sniper:
                projectile = AmmoObjectPool.Instance.GetPooledSniperAmmo();
                break;
            case Type.LMG:
                projectile = AmmoObjectPool.Instance.GetPooledLMGAmmo();
                break;
        }

        if (projectile)
        {
            FireProjectile(projectile);
        }
        else
        {
            Debug.LogWarning($"No available projectile in the pool for {m_eType} weapon!");
        }
    }

    public void FireProjectile(ProjectileBase projectile)
    {
        if (projectile)
        {
            GameObject pooledProjectile = projectile.gameObject;
            pooledProjectile.transform.position = m_MuzzlePosition.transform.position;
            pooledProjectile.transform.rotation = m_MuzzlePosition.transform.rotation;
            switch (m_eType)
            {
                case Type.Pistol:
                    AmmoObjectPool.Instance.PooledPistolAmmo.Remove(projectile);
                    break;
                case Type.Shotgun:
                    AmmoObjectPool.Instance.PooledShotgunAmmo.Remove(projectile);
                    break;
                case Type.SMG:
                    AmmoObjectPool.Instance.PooledSMGAmmo.Remove(projectile);
                    break;
                case Type.AssaultRifle:
                    AmmoObjectPool.Instance.PooledAssaultRifleAmmo.Remove(projectile);
                    break;
                case Type.DMR:
                    AmmoObjectPool.Instance.PooledDMRAmmo.Remove(projectile);
                    break;
                case Type.Sniper:
                    AmmoObjectPool.Instance.PooledSniperAmmo.Remove(projectile);
                    break;
                case Type.LMG:
                    AmmoObjectPool.Instance.PooledLMGAmmo.Remove(projectile);
                    break;
            }
            pooledProjectile.SetActive(true);
            
            // Additional tracking or initialization might be needed here
            m_fTimeSinceLastShot = 0.0f;
        }
    }

    public void Reload() { }
    public void MeleeAttack() { }
   
}
