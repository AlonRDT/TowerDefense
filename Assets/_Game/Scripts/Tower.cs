using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected List<Monster> m_Targets = new List<Monster>();
    [SerializeField] protected GameObject m_TowerHead;
    private float m_AccumulatedTime = 0;
    protected float m_EffectDelay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            m_Targets.Add(other.GetComponent<Monster>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            m_Targets.Remove(other.GetComponent<Monster>());
        }
    }

    protected abstract void noEnemiesNear();

    private void Update()
    {
        m_AccumulatedTime += Time.deltaTime;
        m_Targets.RemoveAll(a => a == null);
        if(m_Targets.Count == 0)
        {
            noEnemiesNear();
        }

        if(m_Targets.Count > 0 && m_AccumulatedTime >= m_EffectDelay)
        {
            m_AccumulatedTime = 0;
            updateWhenEnemyNear();
            EffectEnemy(m_Targets[0]);
        }
    }

    protected abstract void updateWhenEnemyNear();

    protected abstract void EffectEnemy(Monster target);

    protected void DirectHeadAtEnemy(Monster target)
    {
        m_TowerHead.transform.LookAt(target.transform.position + new Vector3(0, 0.5f, 0), m_TowerHead.transform.up);
    }
}
