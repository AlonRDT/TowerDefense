using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Tile> m_Checkpoints;
    [SerializeField] private GameObject m_Monster1;
    [SerializeField] private GameObject m_Monster2;
    [SerializeField] private GameObject m_Monster3;
    private float m_AccumlatedTime;
    private float m_SpawnDelay;
    private float m_WaveDelay;
    private int m_MonsterAmountPerWave;
    private int m_MonstersLeftToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        m_AccumlatedTime = 0;
        m_MonstersLeftToSpawn = 0;
        m_MonsterAmountPerWave = 20;
        m_SpawnDelay = 1.5f;
        m_WaveDelay = 7;

        Settings.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        m_AccumlatedTime += Time.deltaTime;
        if(m_MonstersLeftToSpawn > 0)
        {
            if(m_AccumlatedTime >= m_SpawnDelay)
            {
                m_AccumlatedTime -= m_SpawnDelay;
                m_MonstersLeftToSpawn--;
                GameObject currentMonster = getMonsterToSpawn();
                Monster newMosnster = Instantiate(currentMonster, transform.position + new Vector3(0, 1, 0), currentMonster.transform.rotation).GetComponent<Monster>();
                newMosnster.SetPath(m_Checkpoints);
            }
        }
        else
        {
            if(m_AccumlatedTime >= m_WaveDelay)
            {
                m_AccumlatedTime -= m_WaveDelay;
                Settings.IncreaseWave();
                m_MonstersLeftToSpawn = m_MonsterAmountPerWave;
            }
        }
    }

    private GameObject getMonsterToSpawn()
    {
        GameObject output = null;

        switch (Settings.WaveNumber - 1 % 5)
        {
            case 0:
            case 2:
                output = m_Monster1;
                break;
            case 1:
            case 3:
                output = m_Monster2;
                break;
            case 4:
                output = m_Monster3;
                break;
            default:
                break;
        }

        return output;
    }
}
