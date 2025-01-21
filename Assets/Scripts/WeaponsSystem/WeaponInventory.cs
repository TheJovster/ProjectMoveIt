
using System.Collections.Generic;
using UnityEngine;


namespace WeaponSystem
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] private int m_iCurrentWeapoinIndex;
        [SerializeField] private Weapon m_CurrentWeapon;
        [SerializeField] private List<Weapon> m_WeaponsList = new List<Weapon>();
        
        [SerializeField] private Transform m_WeaponSocket;

        #region Properties

        private void Start()
        {
            if (m_WeaponSocket != null)
            {
                //TODO itterate among the children;
                //TOOD add all of the weapons to the list
                //TODO index 0 is the current weapon
                m_CurrentWeapon = m_WeaponSocket.GetComponentInChildren<Weapon>();
            }
        }

        public int CurrentWeaponIndex
        {
            get
            {
                return m_iCurrentWeapoinIndex;
            }
        }

        public Weapon CurrentWeapon
        {
            get
            {
                return m_CurrentWeapon;
            }
        }

        #endregion
    }
}

