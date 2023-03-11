using Papae.UnitySDK.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIControl : MonoBehaviour
{
    GameObject[] m_goalLocations;
    NavMeshAgent m_agent;
    Animator m_animator;
    float m_speedMult;

    [SerializeField] float m_detectionRadius;
    [SerializeField] float m_fleeRadius;
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioSource m_audioSource1;
    [SerializeField] AudioClip m_audioClipPeopleScream;
    [SerializeField] AudioClip m_audioClipBoneCrush;
    [SerializeField] Animator m_bloodSplat;
    [SerializeField] BoxCollider m_boxCollider;
    void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        m_goalLocations = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0,m_goalLocations.Length);
        m_agent.SetDestination(m_goalLocations[i].transform.position);
        m_animator = this.GetComponent<Animator>();
        m_animator.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        ResetAgent();
        
    }

    
    void Update()
    {
        if(m_agent.remainingDistance < 1)
        {
            ResetAgent();
            int i = Random.Range(0, m_goalLocations.Length);
            m_agent.SetDestination(m_goalLocations[i].transform.position);
        }
        
    }

    public void DetectVehicle(Vector3 position)
    {
        if(Vector3.Distance(position, this.transform.position) < m_detectionRadius && !m_agent.isStopped)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newgoal = this.transform.position + fleeDirection * m_fleeRadius;

            NavMeshPath path = new NavMeshPath();
            m_agent.CalculatePath(newgoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid )
            {
                m_agent.SetDestination(path.corners[path.corners.Length - 1]);
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.PlayOneShot(m_audioClipPeopleScream);
                }
                m_animator.SetTrigger("IsRunning");
                m_agent.speed = 7;
                m_agent.angularSpeed = 500;
            }
        }
    }

    void ResetAgent()
    {
        m_animator.SetTrigger("IsWalking");
        m_speedMult = Random.Range(0.5f, 2f);
        m_animator.SetFloat("speedMult", m_speedMult);
        m_agent.speed *= m_speedMult;
        m_agent.speed = 4;
        m_agent.angularSpeed = 120;
        m_agent.ResetPath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_agent.isStopped = true;
            m_audioSource1.PlayOneShot(m_audioClipBoneCrush);
            m_bloodSplat.SetTrigger("splatBlood");
            m_animator.SetBool("Death_b", true);
            m_animator.SetInteger("DeathType_int", 1);
            GameObject.Destroy(this.gameObject, 2f);
            if(m_boxCollider != null)
                m_boxCollider.isTrigger = false;
        }
    }
}
