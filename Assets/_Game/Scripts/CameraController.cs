using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float m_MaxX = 10;
    [SerializeField] float m_SpeedX = 0.5f;
    [SerializeField] float m_MaxZ = 30;
    [SerializeField] float m_MinZ = -2;
    [SerializeField] float m_SpeedZ = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 6.5f, -2);
        transform.rotation = Quaternion.Euler(new Vector3(60, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float x = Mathf.Clamp(transform.position.x + horizontal * m_SpeedX, -m_MaxX, m_MaxX);
        float z = Mathf.Clamp(transform.position.z + vertical * m_SpeedZ, m_MinZ, m_MaxZ);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
