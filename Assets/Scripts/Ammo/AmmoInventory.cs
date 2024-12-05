using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoInventory : MonoBehaviour
{
    public static AmmoInventory Instance;
    
    private PlayerWeaponInventory m_playerWeaponInventory;
    
    private int m_iPistolAmmoCount;
    private int m_iShotgunAmmoCount;
    private int m_iSMGAmmoCount;
    private int m_iAssaultRifleAmmoCount;
    private int m_iDMRAmmoCount;
    private int m_iSniperAmmoCount;
    private int m_iLMGAmmoCount;

    [SerializeField] private int m_iMaxPistolAmmoCount = 120;
    [SerializeField] private int m_iMaxShotgunAmmoCount = 36;
    [SerializeField] private int m_iMaxSMGAmmoCount = 240;
    [SerializeField] private int m_iMaxAssaultRifleAmmoCount = 360;
    [SerializeField] private int m_iMaxDMRAmmoCount = 100;
    [SerializeField] private int m_iMaxSniperAmmoCount = 60;
    [SerializeField] private int m_iMaxLMGAmmoCount = 600;
    
    #region Properties

    public int CurrentPistolAmmoCount => m_iPistolAmmoCount;
    public int CurrentShotgunAmmoCount => m_iShotgunAmmoCount;
    public int CurrentSMGAmmoCount => m_iSMGAmmoCount;
    public int CurrentAssaultRifleAmmoCount => m_iAssaultRifleAmmoCount;
    public int CurrentDMRAmmoCount => m_iDMRAmmoCount;
    public int CurrentSniperAmmoCount => m_iSniperAmmoCount;
    public int CurrentLMGAmmoCount => m_iLMGAmmoCount;

    #endregion Properties

    private void OnEnable()
    {
        m_iPistolAmmoCount = m_iMaxPistolAmmoCount;
        m_iShotgunAmmoCount = m_iMaxShotgunAmmoCount;
        m_iSMGAmmoCount = m_iMaxSMGAmmoCount;
        m_iAssaultRifleAmmoCount = m_iMaxAssaultRifleAmmoCount;
        m_iDMRAmmoCount = m_iMaxDMRAmmoCount;
        m_iSniperAmmoCount = m_iMaxSniperAmmoCount;
        m_iLMGAmmoCount = m_iMaxLMGAmmoCount;
        m_playerWeaponInventory = GetComponent<PlayerWeaponInventory>();
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
       else if (Instance)
       {
           Destroy(this);
       }
    }

    //so this is gonna be a ducttape solution

    public void DecreaseAmmo(Weapon currentWeapon, int amountToDecrease, int magSize)
    {
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.Pistol)
        {
            m_iPistolAmmoCount -= amountToDecrease;
            if (m_iPistolAmmoCount <= magSize)
            {
                m_iPistolAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.Shotgun)
        {
            m_iShotgunAmmoCount -= amountToDecrease;
            if (m_iShotgunAmmoCount <= magSize)
            {
                m_iPistolAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.SMG)
        {
            m_iSMGAmmoCount -= amountToDecrease;
            if (m_iSMGAmmoCount <= magSize)
            {
                m_iSMGAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.AssaultRifle)
        {
            m_iAssaultRifleAmmoCount -= amountToDecrease;
            if (m_iAssaultRifleAmmoCount <= magSize)
            {
                m_iAssaultRifleAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.DMR)
        {
            m_iDMRAmmoCount -= amountToDecrease;
            if(m_iDMRAmmoCount <= magSize)
            {
                m_iDMRAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.Sniper)
        {
            m_iSniperAmmoCount -= amountToDecrease;
            if (m_iSniperAmmoCount <= magSize)
            {
                m_iShotgunAmmoCount = 0;
            }
        }
        if (currentWeapon.m_WeaponType == Weapon.WeaponType.LMG)
        {
            m_iLMGAmmoCount -= amountToDecrease;
            if (m_iLMGAmmoCount <= magSize)
            {
                m_iLMGAmmoCount = 0;
            }
        }
    }

    public void AddAmmo(ProjectileBase.AmmoType ammoType, int amountToAdd)
    {
        
    }
    
}
