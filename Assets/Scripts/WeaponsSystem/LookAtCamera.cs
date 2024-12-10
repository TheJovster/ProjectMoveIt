using UnityEngine;

namespace WeaponSystem
{
    public class LookAtCamera : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(Camera.main.transform.position - (Vector3.forward * 0.1f));
        }
    }
}

