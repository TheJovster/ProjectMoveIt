using System;
using TMPro;
using UnityEngine;

namespace WeaponSystem
{
    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Instance;
        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        [SerializeField] private TMP_Text m_AmmoInMag;
        [SerializeField] private TMP_Text m_AmmoMax;
        [SerializeField] private TMP_Text m_WeaponName;

        public void UpdateAmmoInMag(int ammoInMag)
        {
            m_AmmoInMag.text = ammoInMag.ToString();
        }

        public void UpdateMaxAmmo(int maxAmmo)
        {
            m_AmmoMax.text = maxAmmo.ToString();
        }

        private void OnDisable()
        {
            Instance = null;
        }

        public void UpdateName(string newName)
        {
            m_WeaponName.text = newName;
        }
    }
}

