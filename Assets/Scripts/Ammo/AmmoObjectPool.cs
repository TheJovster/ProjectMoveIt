using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoObjectPool : MonoBehaviour
{
    public static AmmoObjectPool Instance;
    
    #region Member Variables

    [SerializeField] private int m_iAmmountToPool;
    [SerializeField] private List<ProjectileBase> m_lPooledPistolAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledShotgunAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledSMGAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledDMRAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledAssaultRifleAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledSniperAmmo = new List<ProjectileBase>();
    [SerializeField] private List<ProjectileBase> m_lPooledLMGAmmo = new List<ProjectileBase>();
    //add more Lists here
    
    //other variables
    [SerializeField] private int m_iPistolAmmoToPool;
    [SerializeField] private int m_iShotgunAmmoToPool;
    [SerializeField] private int m_iSMGAmmoToPool;
    [SerializeField] private int m_iDMRAmmoToPool;
    [SerializeField] private int m_iAssaultRifleAmountToPool;
    [SerializeField] private int m_iSniperAmmoPool;
    [SerializeField] private int m_iLMGAmmoPool;
    
    //Ammo types
    [SerializeField] private ProjectileBase m_PistolAmmo;
    [SerializeField] private ProjectileBase m_ShotgunAmmo;
    [SerializeField] private ProjectileBase m_SMGAmmo;
    [SerializeField] private ProjectileBase m_DMRAmmo;
    [SerializeField] private ProjectileBase m_AssaultRifleAmmo;
    [SerializeField] private ProjectileBase m_SniperAmmo;
    [SerializeField] private ProjectileBase m_LMGAmmo;
    
    #endregion

    #region Properties
    public int AmountToPool => m_iAmmountToPool;
    public List<ProjectileBase> PooledPistolAmmo => m_lPooledPistolAmmo;
    public List<ProjectileBase> PooledShotgunAmmo => m_lPooledShotgunAmmo;
    public List<ProjectileBase> PooledSMGAmmo => m_lPooledSMGAmmo;
    public List<ProjectileBase> PooledDMRAmmo => m_lPooledDMRAmmo;
    public List<ProjectileBase> PooledAssaultRifleAmmo => m_lPooledAssaultRifleAmmo;
    public List<ProjectileBase> PooledSniperAmmo => m_lPooledSniperAmmo;
    public List<ProjectileBase> PooledLMGAmmo => m_lPooledPistolAmmo;
    #endregion 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PoolPistolAmmo();
        PoolShotgunAmmo();
        PoolDMRAmmo();
        PoolAssaultRifleAmmo();
        PoolSMGAmmo();
        PoolSniperAmmo();
        PoolLMGAmmo();
        Debug.Log(transform.childCount);
    }

    private void PoolPistolAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iPistolAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_PistolAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolShotgunAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iShotgunAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_ShotgunAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolDMRAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iDMRAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_DMRAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolAssaultRifleAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iAssaultRifleAmountToPool; ++i)
        {
            TempObject = Instantiate(m_AssaultRifleAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolSMGAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iSMGAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_SMGAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolSniperAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iSniperAmmoPool; ++i)
        {
            TempObject = Instantiate(m_SniperAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }

    private void PoolLMGAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iLMGAmmoPool; ++i)
        {
            TempObject = Instantiate(m_LMGAmmo);
            TempObject.gameObject.SetActive(false);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(transform.childCount);
        }
    }
}
