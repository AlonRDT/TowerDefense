using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_AOE : Tower
{
    [SerializeField] private ParticleSystem m_Effect;
    protected override void EffectEnemy(Monster target)
    {
        foreach (var monster in m_Targets)
        {
            monster.Frozen();
        }
    }

    private void Start()
    {
        m_EffectDelay = 0.5f;
        m_Effect.Pause();
    }

    protected override void updateWhenEnemyNear()
    {
        m_TowerHead.transform.Rotate(new Vector3(0, 3 * Time.deltaTime, 0));
        if (m_Effect.isPlaying == false)
        {
            m_Effect.Play();
        }
    }

    protected override void noEnemiesNear()
    {
        m_Effect.Pause();
    }
}
