
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


namespace WeaponSystem
{
    public class WeaponInventory : MonoBehaviour
    {

        
        [FormerlySerializedAs("m_iCurrentWeapoinIndex")] [SerializeField] private int m_iCurrentWeaponIndex;
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
            GenerateWeaponsList();
            DeactivateWeapons();
            SetCurrentWeaponOnInitializationActive(); //what is up with that naming?
        }

        public int CurrentWeaponIndex
        {
            get
            {
                return m_iCurrentWeaponIndex;
            }
        }

        public void GenerateWeaponsList()
        {
            foreach (Weapon weapon in m_WeaponSocket.GetComponentsInChildren<Weapon>())
            {
                m_WeaponsList.Add(weapon);
            }
        }

        public void DeactivateWeapons()
        {
            foreach (Weapon weapon in m_WeaponsList)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        private void SetCurrentWeaponOnInitializationActive()
        {
            if (m_CurrentWeapon == m_WeaponsList[0])
            {
                m_WeaponsList[0].gameObject.SetActive(true);
            }
        }

        public void SwitchWeaponIncrement()
        {
            int previousIndex = m_iCurrentWeaponIndex;
            m_iCurrentWeaponIndex++;
            if (m_iCurrentWeaponIndex != previousIndex)
            {
                if (m_iCurrentWeaponIndex > m_WeaponsList.Count - 1)
                {
                    m_iCurrentWeaponIndex = 0;
                }
                else if (m_iCurrentWeaponIndex < 0)
                {
                    m_iCurrentWeaponIndex = m_WeaponsList.Count - 1;
                }
                m_CurrentWeapon = m_WeaponsList[m_iCurrentWeaponIndex];
                m_WeaponsList[previousIndex].gameObject.SetActive(false);
                m_WeaponsList[m_iCurrentWeaponIndex].gameObject.SetActive(true);
            }
        }

        public void SwitchWeaponDecrement()
        {
            int previousIndex = m_iCurrentWeaponIndex;
            m_iCurrentWeaponIndex--;
            if (m_iCurrentWeaponIndex != previousIndex)
            {
                if (m_iCurrentWeaponIndex > m_WeaponsList.Count - 1)
                {
                    m_iCurrentWeaponIndex = 0;
                }
                else if (m_iCurrentWeaponIndex < 0)
                {
                    m_iCurrentWeaponIndex = m_WeaponsList.Count - 1;
                }
                m_CurrentWeapon = m_WeaponsList[m_iCurrentWeaponIndex];
                m_WeaponsList[previousIndex].gameObject.SetActive(false);
                m_WeaponsList[m_iCurrentWeaponIndex].gameObject.SetActive(true);
            }
        }
        
    }
}

