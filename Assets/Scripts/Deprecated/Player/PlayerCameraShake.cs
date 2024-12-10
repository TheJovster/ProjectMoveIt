using UnityEngine;
using Math;

namespace Characters.Player 
{
    public class PlayerCameraShake : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 5.0f)] private float m_fIntensitity = 0.1f; //default as 0.1. Range from 0.0f to 5.0f. Recommended value between 0.1f and 0.5f
        [SerializeField, Range(0.0f, 100.0f)] private float m_fFrequency = 10.0f; //default as 10.0f, Range from 0.0f to 100.0f, Recommended value is at 10.0f;
        [SerializeField, Range(0.0f, 10.0f)] private float m_fSprintMultiplier = 2.0f; // 2 set as default. Range from 0 to 10;

        private Vector3 m_vOriginalCameraPosition;
        private Quaternion m_qOriginalCameraRotation;
        private PlayerController m_playerController;
        private float m_fShakeTimer;


        #region Initialization
        private void Awake()
        {
            m_playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            m_fShakeTimer = 0.0f;
            m_vOriginalCameraPosition = m_playerController.MainCamera.transform.localPosition;
            m_qOriginalCameraRotation = m_playerController.MainCamera.transform.localRotation;
        }
        #endregion

        public void CameraShakeRun(Camera camera) 
        {

            if (m_playerController.IsSprinting)
            {
                m_fShakeTimer += Time.deltaTime * m_fFrequency * m_fSprintMultiplier ;
                float fXShake = Mathf.Sin(m_fShakeTimer) * m_fIntensitity;
                float fZRotation = Mathf.Sin(m_fShakeTimer) * m_fIntensitity;

                m_playerController.MainCamera.localPosition = m_vOriginalCameraPosition + new Vector3(fXShake, 0.0f, 0.0f);
                m_playerController.MainCamera.localRotation = Quaternion.Euler(0.0f, 0.0f, fZRotation);
            }
            else if (!m_playerController.IsSprinting)
            {
                m_fShakeTimer += Time.deltaTime * m_fFrequency;
                float fXShake = Mathf.Sin(m_fShakeTimer) * m_fIntensitity;
                float fZRotation = Mathf.Sin(m_fShakeTimer) * m_fIntensitity;

                m_playerController.MainCamera.localPosition = m_vOriginalCameraPosition + new Vector3(fXShake, 0.0f, 0.0f);
                m_playerController.MainCamera.localRotation = m_qOriginalCameraRotation * Quaternion.Euler(0.0f, 0.0f, fZRotation);
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
            m_playerController.MainCamera.localPosition = new Vector3(MathUtil.EaseOut(m_vOriginalCameraPosition.x), MathUtil.EaseOut(m_vOriginalCameraPosition.y), MathUtil.EaseOut(m_vOriginalCameraPosition.z));
        }
    }
}

