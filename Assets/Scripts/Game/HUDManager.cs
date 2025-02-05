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
        private bool m_bIsLoading = true;
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
        [SerializeField] private GameObject m_aimReticule;

        
        [Header("Fade In/Out")]
        [SerializeField] private Image m_FadeImage;
        [SerializeField] private float m_fFadeTime = 0.5f;

        private bool m_bShouldFadeIn = false;
        private bool m_bShouldFadeOut = false;

        private void Update()
        {
            if (m_bShouldFadeIn)
            {
                FadeIn();
            }
            else if (!m_bShouldFadeOut)
            {
                //fade out
            }
            
            if (Mathf.Approximately(m_FadeImage.color.a, 1))
            {
                m_bShouldFadeIn = false;
            }
        }

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

        public void EnableAimReticle()
        {
            m_aimReticule.SetActive(true);
        }

        public void DisableAimReticle()
        {
            m_aimReticule.SetActive(false);
        }

        public void SetFadeIn(bool value)
        {
            m_bShouldFadeIn = value;
        }
        
        private void FadeIn()
        {
            Color currentColor = m_FadeImage.color;
            currentColor.a = Mathf.Lerp(currentColor.a, 0, m_fFadeTime * Time.deltaTime);
            if (Mathf.Approximately(currentColor.a, 0))
            {
                currentColor.a = 0.0f;
            }
            m_FadeImage.color = currentColor;
            
        }
    }
}

