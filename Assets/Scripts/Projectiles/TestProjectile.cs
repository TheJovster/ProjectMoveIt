using Characters.Player;
using UnityEngine;

public class TestProjectile : ProjectileBase
{

    private void Update()
    {

        m_RigidBody.AddForce(m_MoveDirection * Time.deltaTime);

        m_LifeTimeCount += Time.deltaTime;
        Debug.Log(Time.deltaTime);
        Debug.Log(m_LifeTimeCount);
        if (m_LifeTimeCount >= m_LifeTime)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroying");
        }
    }

}
