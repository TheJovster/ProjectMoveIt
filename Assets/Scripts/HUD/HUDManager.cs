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

        private void Start()
        {
            UpdateMaxAmmo(PlayerController.Instance.EquippedWeapon.AmmoInventory.GetAmmoCountByType(PlayerController.Instance.EquippedWeapon.Type));
        }


        [SerializeField] private TMP_Text m_AmmoInMag;
        [SerializeField] private TMP_Text m_AmmoMax;

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
            if(Instance != null)
            Instance = null;
        }
    }
}

