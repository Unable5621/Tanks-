using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    public float m_ScrollSpeed = 1.0f;

    public Transform m_target;

    public Vector3 m_MoveVelocity;
    public Vector3 m_DesiredPosition;

    private void Awake()
    {
        //Remove to manually add target to follow instead.
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {
            Camera.main.orthographicSize += scroll * m_ScrollSpeed * -1;
        }
    }
    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, 
         m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}
