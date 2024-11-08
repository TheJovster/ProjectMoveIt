using UnityEngine;


namespace Characters.Player 
{
    public class PlayerCameraShake : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 5.0f)] private float m_fAmplitude;
        [SerializeField, Range(0.0f, 5.0f)] private float m_fFrequency;

        private Vector3 m_vOriginalCameraPosition;

        private void Awake()
        {
            m_vOriginalCameraPosition = Camera.main.transform.localPosition;
        }

        public void CameraShakeRun(Camera camera) 
        {
            float fXShake = Mathf.Sin(m_fAmplitude * m_fFrequency);
            float fYShake = Mathf.Sin(m_fAmplitude * 0.5f * m_fFrequency);

            camera.transform.localPosition += new Vector3(fXShake, fYShake, 0.0f);
            camera.transform.localRotation = Quaternion.Euler(fXShake, fYShake, 0.0f);
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

