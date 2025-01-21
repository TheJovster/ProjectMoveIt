using UnityEngine;


namespace WeaponSystem
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] private int m_iCurrentWeapoinIndex;
        [SerializeField] private Weapon m_CurrentWeapon;

        #region Properties

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

