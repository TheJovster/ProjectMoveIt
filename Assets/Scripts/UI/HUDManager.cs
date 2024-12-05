using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Instance;
        [SerializeField] private GameObject m_Player;
        [SerializeField] private TMP_Text m_AmmoInMag;
        [SerializeField] private TMP_Text m_AmmoInInventory;
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateAmmoCount(int AmmoInMag, int AmmoInInventory)
        {
            m_AmmoInMag.text = AmmoInMag.ToString();
            m_AmmoInInventory.text = AmmoInInventory.ToString();

        }
    }
}
