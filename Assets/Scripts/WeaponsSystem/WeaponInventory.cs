
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace WeaponSystem
{
    public class WeaponInventory : MonoBehaviour
    {

        
        [SerializeField] private int m_iCurrentWeapoinIndex;
        [SerializeField] private Weapon m_CurrentWeapon;
        [SerializeField] private List<Weapon> m_WeaponsList = new List<Weapon>();
        
        [SerializeField] private Transform m_WeaponSocket;

        private AmmoInventory m_AmmoInventory;
        
        
        #region Properties

        public Weapon CurrentWeapon => m_CurrentWeapon;


        #endregion
        
        private void Start()
        {
            if (m_WeaponSocket != null)
            {
                //TODO itterate among the children;
                //TOOD add all of the weapons to the list
                //TODO index 0 is the current weapon
                m_CurrentWeapon = m_WeaponSocket.GetComponentInChildren<Weapon>();
            }

            m_AmmoInventory = GetComponent<AmmoInventory>();
        }

        public int CurrentWeaponIndex
        {
            get
            {
                return m_iCurrentWeapoinIndex;
            }
        }
        
    }
}

