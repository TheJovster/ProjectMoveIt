using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    [SerializeField] private WeaponBase m_currentWeapon;
    [SerializeField] private List<WeaponBase> m_WeaponList = new List<WeaponBase>();
    [SerializeField] private int m_WeaponIndex = -1;

    private void Awake()
    {
        m_currentWeapon = m_WeaponList[m_WeaponIndex];
        if(m_WeaponList.Count < 1) 
        {
            return;
        }
        if (m_WeaponIndex == -1) 
        {
            m_WeaponIndex = 0;
        }

    }

    public void AddWeaponToList(WeaponBase weapon) 
    {
        m_WeaponList.Add(weapon);

    }

    public void EquipWeapon(WeaponBase weapon, int weaponIndex) 
    {

    }

}
