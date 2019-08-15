using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinitStateMashine : MonoBehaviour
{
    /// <summary>
    /// Variables of the finit state mashine
    /// </summary>
    // View radius of the npc 
    public float m_viewRadius;
    // Way points
    public Transform[] m_wayPoints;
    // Reference to the player
    Transform m_player;
    // Reference to the nav mesh
    NavMeshAgent m_agent;

    // Way point selecter
    private int m_selecter = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the player
        m_player = PlayerManager.m_instance.m_player.transform;


        // Gets the nav mesh agent
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance from the npc to the player
        float m_distance = Vector3.Distance(m_player.transform.position, this.transform.position);

        // Distance from the npc to the next way point
        float m_distanceWayPoint = Vector3.Distance(m_wayPoints[m_selecter].transform.position, this.transform.position);

        // Checks the distance to the player
        if (m_distance <= m_viewRadius)
        {
            // Follows the player
            m_agent.SetDestination(m_player.position);
        }
        else
        {
            m_agent.SetDestination(m_wayPoints[m_selecter].position);
            if(m_distanceWayPoint <= 2)
            {
                if (m_selecter == m_wayPoints.Length - 1)
                {
                    m_selecter = 0;
                }
                else
                {
                    ++m_selecter;
                }
            }
        }
    }

    // Shows the selected gizmo
    // the radius of view of the npc
    void OnDrawGizmosSelected()
    {
        // Color of the gizmo
        Gizmos.color = Color.red;
        // Radius of the sphere gizmo
        Gizmos.DrawWireSphere(transform.position, m_viewRadius);
    }
}
