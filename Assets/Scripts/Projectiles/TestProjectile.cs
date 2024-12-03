using Characters.Player;
using UnityEngine;

public class TestProjectile : ProjectileBase
{

    private void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            m_RigidBody.AddForce(transform.forward * (m_fVelocity * Time.deltaTime), ForceMode.Impulse);

            m_LifeTimeCount += Time.deltaTime;
            if (m_LifeTimeCount >= m_LifeTime)
            {
                DeactivateObject();
            }
        }

    }

    private void DeactivateObject()
    {
        this.gameObject.SetActive(false);
        //Debug.Log("Deactivating");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon")) 
        {
            return;
        }
        else 
        {
            //check if has health component
            DeactivateObject();
            
        }
    }

}
