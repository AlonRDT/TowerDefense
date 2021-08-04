using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Cannon : Tower
{
    [SerializeField] GameObject m_ProjectilePrefab;

    protected override void EffectEnemy(Monster target)
    {
        Projectile_Cannon projectile = Instantiate(m_ProjectilePrefab, m_TowerHead.transform.position, m_TowerHead.transform.rotation).GetComponent<Projectile_Cannon>();
        projectile.Initialize(target);
    }

    protected override void noEnemiesNear()
    {
        m_TowerHead.transform.rotation = Quaternion.identity;
    }

    protected override void updateWhenEnemyNear()
    {
        DirectHeadAtEnemy(m_Targets[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_EffectDelay = 1f;   
    }
}
