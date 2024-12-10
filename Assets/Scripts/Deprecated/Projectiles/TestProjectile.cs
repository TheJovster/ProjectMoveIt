using Characters.Player;
using UnityEngine;

public class TestProjectile : ProjectileBase
{
    private void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.m_RigidBody.AddForce(transform.forward * (m_fVelocity * Time.deltaTime), ForceMode.Impulse);

            this.m_LifeTimeCount += Time.deltaTime;
            if (this.m_LifeTimeCount >= m_LifeTime)
            {
                DeactivateObject();
            }
        }
    }

    protected override void DeactivateObject()
    {
        this.gameObject.SetActive(false);
        switch(Type)
        {
            case AmmoType.Pistol:
                AmmoObjectPool.Instance.PooledPistolAmmo.Add(this);
                break;
            case AmmoType.Shotgun:
                AmmoObjectPool.Instance.PooledShotgunAmmo.Add(this);
                break;
            case AmmoType.SMG: 
                AmmoObjectPool.Instance.PooledSMGAmmo.Add(this);
                break;
            case AmmoType.AssaultRifle:
                AmmoObjectPool.Instance.PooledAssaultRifleAmmo.Add(this);
                break;
            case AmmoType.DMR:
                AmmoObjectPool.Instance.PooledDMRAmmo.Add(this);
                break;
            case AmmoType.Sniper:
                AmmoObjectPool.Instance.PooledSniperAmmo.Add(this);
                break;
            case AmmoType.LMG:
                AmmoObjectPool.Instance.PooledLMGAmmo.Add(this);
                break;
        }
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
