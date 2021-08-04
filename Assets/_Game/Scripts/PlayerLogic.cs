using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic Instance;

    [SerializeField] private Text m_MoneyText;
    [SerializeField] private Text m_HealthText;
    private int m_Money;
    private int m_Health;
    private float m_AccumulatedTime;
    private float m_MoneyDelay;
    private ETowerType m_BuildTower;
    private Tile m_TargetTile;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_Health = 40;
        m_Money = 150;
        visualizeHealth();
        visualizeMoney();
        m_AccumulatedTime = 0;
        m_MoneyDelay = 1;
        m_BuildTower = ETowerType.None;
    }

    // Update is called once per frame
    void Update()
    {
        m_AccumulatedTime += Time.deltaTime;
        if (m_AccumulatedTime >= m_MoneyDelay)
        {
            m_AccumulatedTime -= m_MoneyDelay;
            increaseMoneyFromTime();
            visualizeMoney();
        }

        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (m_TargetTile != null)
                {
                    m_TargetTile.Preview(false, m_BuildTower);
                }
                m_BuildTower = ETowerType.None;
            }

            Tile newTile = null;
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 100);

            foreach (var hit in hits)
            {
                newTile = hit.transform.GetComponent<Tile>();
                if(newTile != null)
                {
                    break;
                }
            }

            if (newTile != m_TargetTile)
            {
                if (m_TargetTile != null)
                {
                    m_TargetTile.Preview(false, m_BuildTower);
                }
                m_TargetTile = newTile;
                if (m_TargetTile != null)
                {
                    m_TargetTile.Preview(true, m_BuildTower);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (m_TargetTile != null)
                {
                    m_TargetTile.Build(m_BuildTower);
                    reduceMoneyForBuild();
                }
            }
        }
        else
        {
            if (m_TargetTile != null)
            {
                m_TargetTile.Preview(false, m_BuildTower);
            }
            m_TargetTile = null;
        }
    }

    private void reduceMoneyForBuild()
    {
        switch (m_BuildTower)
        {
            case ETowerType.None:
                break;
            case ETowerType.Cannon:
                m_Money -= Settings.CannonPrice;
                break;
            case ETowerType.Beam:
                m_Money -= Settings.BeamPrice;
                break;
            case ETowerType.AOE:
                m_Money -= Settings.AOEPrice;
                break;
            default:
                break;
        }

        m_BuildTower = ETowerType.None;
    }

    public void SelectTowerToBuild(int type)
    {
        switch (type)
        {
            case 0:
                if (m_Money >= Settings.CannonPrice)
                {
                    m_BuildTower = ETowerType.Cannon;
                }
                else
                {
                    m_BuildTower = ETowerType.None;
                }
                break;
            case 1:
                if (m_Money >= Settings.BeamPrice)
                {
                    m_BuildTower = ETowerType.Beam;
                }
                else
                {
                    m_BuildTower = ETowerType.None;
                }
                break;
            case 2:
                if (m_Money >= Settings.AOEPrice)
                {
                    m_BuildTower = ETowerType.AOE;
                }
                else
                {
                    m_BuildTower = ETowerType.None;
                }
                break;
            default:
                break;
        }
    }

    private void increaseMoneyFromTime()
    {
        m_Money += Settings.WaveNumber;
    }

    public void IncreaseMoneyFromMonsterKill()
    {
        m_Money += 5 * Settings.WaveNumber;
    }

    private void visualizeHealth()
    {
        m_HealthText.text = m_Health.ToString();
    }

    private void visualizeMoney()
    {
        m_MoneyText.text = m_Money.ToString();
    }

    public void TakeDamage()
    {
        m_Health--;
        visualizeHealth();

        if (m_Health <= 0)
        {
            gameOver();
        }
    }



    private void gameOver()
    {
        throw new NotImplementedException();
    }
}
