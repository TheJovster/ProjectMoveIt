using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoObjectPool : MonoBehaviour
{
    public static AmmoObjectPool Instance;
    
    #region Member Variables

    [SerializeField] private GameObject AmmoPoolParent;
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
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolShotgunAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iShotgunAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_ShotgunAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolDMRAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iDMRAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_DMRAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolAssaultRifleAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iAssaultRifleAmountToPool; ++i)
        {
            TempObject = Instantiate(m_AssaultRifleAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolSMGAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iSMGAmmoToPool; ++i)
        {
            TempObject = Instantiate(m_SMGAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolSniperAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iSniperAmmoPool; ++i)
        {
            TempObject = Instantiate(m_SniperAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }

    private void PoolLMGAmmo()
    {
        ProjectileBase TempObject;
        for (int i = 0; i < m_iLMGAmmoPool; ++i)
        {
            TempObject = Instantiate(m_LMGAmmo);
            TempObject.gameObject.SetActive(false);
            TempObject.gameObject.transform.SetParent(AmmoPoolParent.transform);
            m_lPooledPistolAmmo.Add(TempObject);
            Debug.Log(AmmoPoolParent.transform.childCount);
        }
    }
    
    //getters

    public ProjectileBase GetPooledPistolAmmo()
    {    
        for(int i = 0; i < m_iPistolAmmoToPool; i++)
        {
            if(!m_lPooledPistolAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledPistolAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledShotgunAmmo()
    {
        for(int i = 0; i < m_iShotgunAmmoToPool; i++)
        {
            if(!m_lPooledShotgunAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledShotgunAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledSMGAmmo()
    {
        for(int i = 0; i < m_iSMGAmmoToPool; i++)
        {
            if(!m_lPooledSMGAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledSMGAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledAssaultRifleAmmo()
    {
        for(int i = 0; i < m_iAssaultRifleAmountToPool; i++)
        {
            if(!m_lPooledAssaultRifleAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledAssaultRifleAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledDMRAmmo()
    {
        for(int i = 0; i < m_iDMRAmmoToPool; i++)
        {
            if(!m_lPooledDMRAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledDMRAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledSniperAmmo()
    {
        for(int i = 0; i < m_iSniperAmmoPool; i++)
        {
            if(!m_lPooledSniperAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledSniperAmmo[i];
            }
        }
        return null;
    }

    public ProjectileBase GetPooledLMGAmmo()
    {
        for(int i = 0; i < m_iLMGAmmoPool; i++)
        {
            if(!m_lPooledLMGAmmo[i].gameObject.activeInHierarchy)
            {
                return m_lPooledLMGAmmo[i];
            }
        }
        return null;
    }
}
