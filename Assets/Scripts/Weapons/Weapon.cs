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
    [SerializeField]public WeaponType m_WeaponType;
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

    public enum WeaponType 
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
        if (m_WeaponType == WeaponType.AssaultRifle || m_WeaponType == WeaponType.LMG || m_WeaponType == WeaponType.SMG)
        {
            m_bIsFullAuto = true;
        }
        else if (m_WeaponType == WeaponType.Pistol || m_WeaponType == WeaponType.DMR || m_WeaponType == WeaponType.Sniper || m_WeaponType == WeaponType.Shotgun)
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

        switch (m_WeaponType)
        {
            case WeaponType.Pistol:
                projectile = AmmoObjectPool.Instance.GetPooledPistolAmmo();
                break;
            case WeaponType.Shotgun:
                projectile = AmmoObjectPool.Instance.GetPooledShotgunAmmo();
                break;
            case WeaponType.SMG:
                projectile = AmmoObjectPool.Instance.GetPooledSMGAmmo();
                break;
            case WeaponType.AssaultRifle:
                projectile = AmmoObjectPool.Instance.GetPooledAssaultRifleAmmo();
                break;
            case WeaponType.DMR:
                projectile = AmmoObjectPool.Instance.GetPooledDMRAmmo();
                break;
            case WeaponType.Sniper:
                projectile = AmmoObjectPool.Instance.GetPooledSniperAmmo();
                break;
            case WeaponType.LMG:
                projectile = AmmoObjectPool.Instance.GetPooledLMGAmmo();
                break;
        }

        if (projectile)
        {
            FireProjectile(projectile);
        }
        else
        {
            Debug.LogWarning($"No available projectile in the pool for {m_WeaponType} weapon!");
        }
    }

    public void FireProjectile(ProjectileBase projectile)
    {
        if (projectile)
        {
            GameObject pooledProjectile = projectile.gameObject;
            pooledProjectile.transform.position = m_MuzzlePosition.transform.position;
            pooledProjectile.transform.rotation = m_MuzzlePosition.transform.rotation;
            switch (m_WeaponType)
            {
                case WeaponType.Pistol:
                    AmmoObjectPool.Instance.PooledPistolAmmo.Remove(projectile);
                    break;
                case WeaponType.Shotgun:
                    AmmoObjectPool.Instance.PooledShotgunAmmo.Remove(projectile);
                    break;
                case WeaponType.SMG:
                    AmmoObjectPool.Instance.PooledSMGAmmo.Remove(projectile);
                    break;
                case WeaponType.AssaultRifle:
                    AmmoObjectPool.Instance.PooledAssaultRifleAmmo.Remove(projectile);
                    break;
                case WeaponType.DMR:
                    AmmoObjectPool.Instance.PooledDMRAmmo.Remove(projectile);
                    break;
                case WeaponType.Sniper:
                    AmmoObjectPool.Instance.PooledSniperAmmo.Remove(projectile);
                    break;
                case WeaponType.LMG:
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
