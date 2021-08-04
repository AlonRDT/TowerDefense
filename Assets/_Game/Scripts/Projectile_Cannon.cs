using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile_Cannon : Projectile
{
    private NavMeshAgent m_Agent;
    private Monster m_Target;
    private bool m_IsInitialized;

    // Start is called before the first frame update
    void Awake()
    {
        Damage = 4;
        m_Agent = GetComponent<NavMeshAgent>();
        m_IsInitialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsInitialized)
        {
            if (m_Target == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                m_Agent.destination = m_Target.transform.position;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster" && m_Target != null && other.gameObject == m_Target.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(Monster target)
    {
        m_IsInitialized = true;
        m_Target = target;
    }
}
