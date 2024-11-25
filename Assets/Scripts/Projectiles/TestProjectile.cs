using Characters.Player;
using UnityEngine;

public class TestProjectile : ProjectileBase
{

    private void Update()
    {

        m_RigidBody.AddForce(transform.forward * m_fVelocity * Time.deltaTime, ForceMode.Impulse);

        m_LifeTimeCount += Time.deltaTime;
        Debug.Log(Time.deltaTime);
        Debug.Log(m_LifeTimeCount);
        if (m_LifeTimeCount >= m_LifeTime)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroying");
        }
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
            Destroy(this.gameObject);
            
        }
    }

}
