using Unity.VisualScripting;
using UnityEngine;


namespace WeaponSystem
{
    public class AmmoInventory : MonoBehaviour
    {
        private PlayerController m_Player;
        private Weapon m_CurrentWeapon;
    
        //ammo count

        [SerializeField] private int m_CurrentPistolAmmo;
        [SerializeField] private int m_MaxPistolAmmo = 120;
        [SerializeField] private int m_CurrentAssaultRifleAmmo;
        [SerializeField] private int m_MaxAssaultRifleAmmo = 300;
        [SerializeField] private int m_CurrentSMGAmmo;
        [SerializeField] private int m_MaxSMGAmmo = 300;
        [SerializeField] private int m_CurrentDMRAmmo;
        [SerializeField] private int m_MaxDMRAmmo = 90;
        [SerializeField] private int m_CurrentSniperAmmo;
        [SerializeField] private int m_MaxSniperAmmo = 120;
        [SerializeField] private int m_CurrentLMGAmmo;
        [SerializeField] private int m_MaxLMGAmmo = 600;
    
    
        private void OnEnable()
        {
            m_Player = GetComponent<PlayerController>();
        }

        private void Awake()
        {
            m_CurrentPistolAmmo = m_MaxPistolAmmo;
            m_CurrentAssaultRifleAmmo = m_MaxAssaultRifleAmmo;
            m_CurrentSMGAmmo = m_MaxSMGAmmo;
            m_CurrentDMRAmmo = m_MaxDMRAmmo;
            m_CurrentLMGAmmo = m_MaxLMGAmmo;
            m_CurrentSniperAmmo = m_MaxLMGAmmo;
        }

        public void DecreaseAmmoCount(int amountToDecrease, Weapon.WeaponType type)
        {
            switch (type)
            {
                case Weapon.WeaponType.Pistol:
                    if(m_CurrentPistolAmmo > 0)
                        m_CurrentPistolAmmo -= amountToDecrease;
                    break;
                case Weapon.WeaponType.AssaultRifle:
                    if(m_CurrentAssaultRifleAmmo > 0)
                        m_CurrentAssaultRifleAmmo -= amountToDecrease;
                    break;
                case Weapon.WeaponType.SMG:
                    if(m_CurrentSMGAmmo > 0)
                        m_CurrentSMGAmmo -= amountToDecrease;
                    break;
                case Weapon.WeaponType.DMR:
                    if(m_CurrentDMRAmmo > 0)
                        m_CurrentDMRAmmo -= amountToDecrease;
                    break;
                case Weapon.WeaponType.Sniper:
                    if(m_CurrentSniperAmmo > 0)
                        m_CurrentSniperAmmo -= amountToDecrease;
                    break;
                case Weapon.WeaponType.LMG:
                    if(m_CurrentLMGAmmo > 0)
                        m_CurrentLMGAmmo -= amountToDecrease;
                    break;
            }
        }

        public int GetAmmoCountByType(Weapon.WeaponType type)
        {
            int returnIndex = 0;
            switch (type)
            {
                case Weapon.WeaponType.Pistol:
                    returnIndex = m_CurrentPistolAmmo;
                    break;
                case Weapon.WeaponType.AssaultRifle:
                    returnIndex = m_CurrentAssaultRifleAmmo;
                    break;
                case Weapon.WeaponType.SMG:
                    returnIndex = m_CurrentSMGAmmo;
                    break;
                case Weapon.WeaponType.DMR:
                    returnIndex = m_CurrentDMRAmmo;
                    break;
                case Weapon.WeaponType.Sniper:
                    returnIndex = m_CurrentSniperAmmo;
                    break;
                case Weapon.WeaponType.LMG:
                    returnIndex = m_CurrentLMGAmmo;
                    break;
            }
            
            return returnIndex;
        }
    }
}

