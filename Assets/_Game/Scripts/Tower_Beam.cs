using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Beam : Tower
{
    [SerializeField] private GameObject m_Beam;
    private float m_DamagePerSecond = 10;

    protected override void EffectEnemy(Monster target)
    {
        target.TakeDamage(m_DamagePerSecond * Time.deltaTime);
    }

    protected override void noEnemiesNear()
    {
        m_TowerHead.transform.rotation = Quaternion.identity;
        m_Beam.SetActive(false);
    }

    protected override void updateWhenEnemyNear()
    {
        DirectHeadAtEnemy(m_Targets[0]);
        m_Beam.SetActive(true);
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_EffectDelay = 0.01f;
        m_Beam.SetActive(false);
    }
}
