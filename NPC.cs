using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    /// <summary>
    /// Variables NPC
    /// </summary>
    // AI
    public AI m_CurrentAI;
    public float m_minDistanceToPlayer = 5;
    public float m_maxDistanceToPlayer = 5;
    // Get Player
    //private PlayerScript m_Player
    //{
    //    get
    //    {
    //        return m_Player;
    //    }
    //}
    //PlayerScript m_Player;
    // Renderer
    Renderer[] m_renderer;

    private ENPCState m_currentState;

    public ENPCState State
    {
        get { return m_currentState; }
        set
        {
            if (m_currentState == value)
                return;

            DeactiveOldState();
            m_currentState = value;
            //ActivateNewState();
        }
    }

    // NavMeshAgent
    private NavMeshAgent m_agent;

    private void DeactiveOldState()
    {
        switch (State)
        {
            case ENPCState.IDLE:
                break;
            case ENPCState.FOLLOW:
                m_agent.speed /= 2f;
                break;
            default:
                break;
        }
    }

    // ENUM stats of NPC
    public enum ENPCState
    {
        IDLE,
        FOLLOW
    }

    void Awake()
    {
        m_renderer = GetComponentsInChildren<Renderer>();
        //m_Player = FindObjectOfType<PlayerScript>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start ()
    {
        State = ENPCState.IDLE;
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                State = (ENPCState)i;
            }
        }

        switch (State)
        {
            case ENPCState.IDLE:
                DoIdleStuff();
                break;
            case ENPCState.FOLLOW:
                DoFollowStuff();
                break;
            default:
                break;
        }
    }

    private void DoIdleStuff()
    {
        //if (Vector3.Distance(m_Player.transform.position, transform.position) < m_CurrentAI.m_minDistanceToPlayer)
        //{
        //    State = ENPCState.FOLLOW;
        //    return;
        //}
    }

    private void DoFollowStuff()
    {
        //m_agent.SetDestination(transform.position + (transform.position - m_Player.transform.position).normalized * 11);
        //
        //if (Vector3.Distance(m_Player.transform.position, transform.position) > m_minDistanceToPlayer * 2)
        //{
        //    State = ENPCState.FOLLOW;
        //}
        //else
        //{
        //    State = ENPCState.IDLE;
        //}
    }
}
