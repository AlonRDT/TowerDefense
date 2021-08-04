using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool m_IsPath;
    [SerializeField] GameObject m_CannonTower;
    [SerializeField] GameObject m_PreviewCannonTower;
    [SerializeField] GameObject m_BeamTower;
    [SerializeField] GameObject m_PreviewBeamTower;
    [SerializeField] GameObject m_AOETower;
    [SerializeField] GameObject m_PreviewAOETower;
    private ETowerType m_CurrentTower;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTower = ETowerType.None;
    }

    public void Preview(bool isActive, ETowerType type)
    {
        if (m_IsPath == false && m_CurrentTower == ETowerType.None)
        {
            switch (type)
            {
                case ETowerType.None:
                    break;
                case ETowerType.Cannon:
                    m_PreviewCannonTower.SetActive(isActive);
                    break;
                case ETowerType.Beam:
                    m_PreviewBeamTower.SetActive(isActive);
                    break;
                case ETowerType.AOE:
                    m_PreviewAOETower.SetActive(isActive);
                    break;
                default:
                    break;
            }
        }
    }

    public void Build(ETowerType type)
    {
        if (m_IsPath == false && m_CurrentTower == ETowerType.None)
        {
            m_PreviewCannonTower.SetActive(false);
            m_PreviewBeamTower.SetActive(false);
            m_PreviewAOETower.SetActive(false);
            switch (type)
            {
                case ETowerType.None:
                    break;
                case ETowerType.Cannon:
                    m_CannonTower.SetActive(true);
                    break;
                case ETowerType.Beam:
                    m_BeamTower.SetActive(true);
                    break;
                case ETowerType.AOE:
                    m_AOETower.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
