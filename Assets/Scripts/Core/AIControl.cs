using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] m_goalLocations;
    NavMeshAgent m_agent;
    Animator m_animator;
    void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        m_goalLocations = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0,m_goalLocations.Length);
        m_agent.SetDestination(m_goalLocations[i].transform.position);
        m_animator = this.GetComponent<Animator>();
        m_animator.SetTrigger("IsWalking");
        m_animator.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        float speedMultiplier = Random.Range(0.5f, 2f);
        m_animator.SetFloat("speedMult", speedMultiplier);
        m_agent.speed *= speedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_agent.remainingDistance < 1)
        {
            int i = Random.Range(0, m_goalLocations.Length);
            m_agent.SetDestination(m_goalLocations[i].transform.position);
        }
        
    }
}
