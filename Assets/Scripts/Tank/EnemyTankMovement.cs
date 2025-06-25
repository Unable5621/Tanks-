using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class EnemyTankMovement : MonoBehaviour
{
    public float m_CloseDistance = 8f;
    public Transform m_Turret;

    private GameObject m_Player;
    public Transform m_SpawnPoint;

    private Transform m_CurrentTarget;

    private NavMeshAgent m_NavAgent;
    private Rigidbody m_Rigidbody;

    private bool m_Follow;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (m_SpawnPoint != null)
            m_CurrentTarget = m_SpawnPoint;
        else
            m_CurrentTarget = transform;

        m_Rigidbody.isKinematic = false;
        //Teleport to spawn point
        transform.position = m_SpawnPoint.position;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CurrentTarget = m_Player.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (m_SpawnPoint != null)
                m_CurrentTarget = m_SpawnPoint;
            else
                m_CurrentTarget = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        float distance = (m_Player.transform.position - transform.position).magnitude;
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_CurrentTarget.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            m_NavAgent.isStopped = true;
        }

        if (m_Turret != null)
        {
            if (m_CurrentTarget != m_Player.transform)
            {
                //When not targeting the player,
                //look forward so that it doesn't look straight down when reaching the spawn point,
                //and it also looks more natural when traveling back to it's spawn
                m_Turret.gameObject.transform.rotation = transform.rotation;
            }
            else
            {
                m_Turret.LookAt(m_CurrentTarget);
            }
        }
    }
}
