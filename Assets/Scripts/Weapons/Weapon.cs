using System;
using UI;
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
    [SerializeField] public WeaponType m_WeaponType;
    [SerializeField] private float m_fRateOfFire;
    [SerializeField] private int m_magSize;
    [SerializeField] private int m_CurrentAmmoInMag;
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
        else if (m_WeaponType == WeaponType.Pistol || m_WeaponType == WeaponType.DMR ||
                 m_WeaponType == WeaponType.Sniper || m_WeaponType == WeaponType.Shotgun)
        {
            m_bIsFullAuto = false;
        }

        m_CurrentAmmoInMag = m_magSize;
        HUDManager.Instance.UpdateAmmoCount(m_CurrentAmmoInMag, GetCurrentAmmoInInventory());
    }

    private void Update()
    {
        m_fTimeSinceLastShot += Time.deltaTime;
        if (m_fTimeSinceLastShot >= 100.0f)
        {
            m_fTimeSinceLastShot = 100.0f;
        }
    }

    private int GetCurrentAmmoInInventory()
    {
        switch (m_WeaponType)
        {
            case WeaponType.Pistol:
                return AmmoInventory.Instance.CurrentPistolAmmoCount;
                break;
            case WeaponType.Shotgun:
                return AmmoInventory.Instance.CurrentShotgunAmmoCount;
                break;
            case WeaponType.SMG:
                return AmmoInventory.Instance.CurrentSMGAmmoCount;
                break;
            case WeaponType.AssaultRifle :
                return AmmoInventory.Instance.CurrentAssaultRifleAmmoCount;
                break;
            case WeaponType.DMR:
                return AmmoInventory.Instance.CurrentDMRAmmoCount;
                break;
            case WeaponType.Sniper:
                return AmmoInventory.Instance.CurrentSniperAmmoCount;
                break;
            case WeaponType.LMG:
                return AmmoInventory.Instance.CurrentLMGAmmoCount;
                break;
            default:
                return 0;
                break;
        }

        return 0;
    }

public void Equip()
    {
        m_bIsActive = true;
        gameObject.SetActive(true);
    }

    public void Uneqip()
    {
        this.m_bIsActive = false;
        this.gameObject.SetActive(false);

    }

    public void Fire()
    {
        if (m_CurrentAmmoInMag > 0)
        {
            ProjectileBase projectile = null;

            switch (m_WeaponType)
            {

                case WeaponType.Pistol:
                    projectile = AmmoObjectPool.Instance.GetPooledPistolAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.Shotgun:
                    projectile = AmmoObjectPool.Instance.GetPooledShotgunAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.SMG:
                    projectile = AmmoObjectPool.Instance.GetPooledSMGAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.AssaultRifle:
                    projectile = AmmoObjectPool.Instance.GetPooledAssaultRifleAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.DMR:
                    projectile = AmmoObjectPool.Instance.GetPooledDMRAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.Sniper:
                    projectile = AmmoObjectPool.Instance.GetPooledSniperAmmo();
                    m_CurrentAmmoInMag--;
                    break;
                case WeaponType.LMG:
                    projectile = AmmoObjectPool.Instance.GetPooledLMGAmmo();
                    m_CurrentAmmoInMag--;
                    break;
            }

            if (projectile)
            {
                FireProjectile(projectile);
            }
        }
        else
        {
            //try reload mag
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
