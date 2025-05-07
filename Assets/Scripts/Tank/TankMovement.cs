using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float m_Speed = 20f;
    public float m_RotationSpeed = 180f;

    private Rigidbody m_Rigidbody;

    private float m_ForwardInputValue;
    private float m_TurnInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        //When the tank is on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;

        //also reset the input values
        m_ForwardInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        //When the tank is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        m_ForwardInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 wantedVelocity = transform.forward * m_ForwardInputValue * m_Speed;

        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

    private void Turn()
    {
        float turnValue = m_TurnInputValue * m_RotationSpeed *Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turnValue, 0f);

        m_Rigidbody.MoveRotation(transform.rotation * turnRotation);
    }
}
