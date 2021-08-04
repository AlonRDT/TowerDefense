using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] bool m_IsFlying;
    [SerializeField] private int m_BaseHealth;
    private float m_Health;
    private NavMeshAgent m_Agent;
    private List<Tile> m_Path;
    private int m_PathDestination;
    private bool m_IsFrozen;
    private float m_AccumlatedFreezeTime; 

    // Start is called before the first frame update
    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Health = m_BaseHealth * Settings.MonsterHealthMultiplier;
        m_PathDestination = -1;
        m_IsFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsFrozen)
        {
            m_AccumlatedFreezeTime += Time.deltaTime;
            if(m_AccumlatedFreezeTime >= Settings.FreezeDelay)
            {
                m_IsFrozen = false;
                m_Agent.speed = Settings.BaseSpeed;
            }
        }

        if (m_PathDestination >= 0 && transform.position.x == m_Path[m_PathDestination].transform.position.x && transform.position.z == m_Path[m_PathDestination].transform.position.z)
        {
            m_PathDestination++;
            if (m_PathDestination == m_Path.Count)
            {
                PlayerLogic.Instance.TakeDamage();
                Destroy(gameObject);
            }
            else
            {
                setDestination();
            }
        }
    }

    public void SetPath(List<Tile> path)
    {
        m_Path = path;

        if (m_IsFlying == true)
        {
            m_PathDestination = m_Path.Count - 1;
        }
        else
        {
            m_PathDestination = 0;
        }

        setDestination();
    }

    private void setDestination()
    {
        m_Agent.destination = m_Path[m_PathDestination].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            Debug.Log(projectile == null);
            TakeDamage(projectile.Damage);
        }
    }

    public void TakeDamage(float damage)
    {
        m_Health -= damage;

        if (m_Health <= 0)
        {
            PlayerLogic.Instance.IncreaseMoneyFromMonsterKill();
            Destroy(gameObject);
        }
    }

    public void Frozen()
    {
        m_IsFrozen = true;
        m_AccumlatedFreezeTime = 0;
        m_Agent.speed = Settings.FreezeSpeed;
    }
}
