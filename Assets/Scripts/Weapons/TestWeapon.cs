using UnityEngine;

public class TestWeapon : WeaponBase
{

    private void Awake()
    {
/*        m_MuzzlePosition = transform.parent.Find("MuzzlePosition").gameObject;
        Debug.Log(m_MuzzlePosition.name);*/
    }

    private void Start()
    {

    }

    public override void Fire()
    {
        base.Fire();
        ProjectileBase projectileInstance = Instantiate(
            m_weaponProjectile, 
            m_MuzzlePosition.transform.position,
            m_MuzzlePosition.transform.rotation);
    }

    public override void Reload()
    {
        base.Reload();
    }

    public override void MeleeAttack()
    {
        base.MeleeAttack();
    }

}

