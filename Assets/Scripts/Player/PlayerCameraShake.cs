using UnityEngine;

namespace Characters.Player 
{
    public class PlayerCameraShake : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 5.0f)] private float m_fIntensitity;
        [SerializeField, Range(0.0f, 5.0f)] private float m_fFrequency;
        [SerializeField, Range(0.0f, 10.0f)] private float m_sprintMultiplier = 2.0f; // 2 set as default. Range from 0 to 10;

        private Vector3 m_vOriginalCameraPosition;
        private Quaternion m_qOriginalCameraPosition;
        private PlayerController m_playerController;

        private void Awake()
        {
            m_vOriginalCameraPosition = Camera.main.transform.localPosition;
            m_qOriginalCameraPosition = Camera.main.transform.localRotation;
            m_playerController = GetComponent<PlayerController>();

        }

        public void CameraShakeRun(Camera camera) 
        {
            if (m_playerController.IsSprinting)
            {

            }
            else if (!m_playerController.IsSprinting)
            {
                 
            }


        }

        public void CameraShakeTakeDamage(Camera camera) 
        {

        }

        public void CameraShakeShoot(Camera camera) 
        {

        }

        public void ReturnCameraToNormal(Camera camera) 
        {

        }
    }
}

