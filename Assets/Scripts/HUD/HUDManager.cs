using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
        [SerializeField] private List<Image> m_FireRateImages = new List<Image>();
        [SerializeField] private bool m_bFireRateCheck;

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

        public void UpdateFireRateImage(bool value)
        {
            m_bFireRateCheck = value;
            if(m_bFireRateCheck)
            {
                m_FireRateImages[0].enabled = true;
                m_FireRateImages[1].enabled = false;
            }
            else if(!m_bFireRateCheck)
            {
                m_FireRateImages[1].enabled = true;
               m_FireRateImages[0].enabled = false;
            }
        }
    }
}

