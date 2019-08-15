using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    // Enemy Stats
    public int m_Hearths = 6;
    public float m_MaxAttackCD;
    float m_attackCD;
    bool m_Dash;

    // 0 = nothing | 1 = left attack/block | 2 = right attack/block
    public int m_isAttacking = 0;
    public int m_isBlocking = 0;

    // Attack or Block | Attack Number | Block Number
    int m_AttackOrBlock;
    int m_attackNumber;
    int m_blockNumber;

    // check for player wuithin range
    public float m_AttackingRange;

    // Checking if animation is playing
    bool m_isAnimationPlaying = false;

    //
    NavMeshAgent m_agent;

    // sword
    public GameObject m_sword;
    Animation m_animation;
    public SchwertCollisionEnabler m_swordCollision;

    PlayerScript m_player;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        m_agent = GetComponent<NavMeshAgent>();

        m_animation = GetComponentInChildren<Animation>();

        m_swordCollision = m_sword.GetComponent<SchwertCollisionEnabler>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Enemy: " + m_Hearths);

        m_isAnimationPlaying = IsActionAnimationPlaying();
        IsPlayerInRange();
        Movement(m_isAnimationPlaying);
    }

    void Movement(bool _isAnimationPlaying)
    {
        if (!_isAnimationPlaying)
        {
            //transform.LookAt(m_player.transform);
            //Play Walk animation
            if (m_agent.velocity != new Vector3(0, 0, 0))
            {
                m_animation.Play("Run");
            }
            else
            {
            }

            if ((m_player.transform.position - transform.position).sqrMagnitude > (m_agent.stoppingDistance * m_agent.stoppingDistance))
            {
                m_agent.SetDestination(m_player.transform.position);
            }
        }
    }

    void IsPlayerInRange()
    {
        if ((m_player.transform.position - transform.position).sqrMagnitude <= m_AttackingRange * m_AttackingRange)
        {
            //m_AttackOrBlock = Random.Range(0, 1);
            //if (m_AttackOrBlock == 0)
            //{
            //    //Block(m_isAnimationPlaying);
            //}
            //else if (m_AttackOrBlock == 1)
            {
                Attack(m_isAnimationPlaying);
            }
        }
    }

    void Attack(bool _isAnimationPlaying)
    {
        m_attackCD -= Time.deltaTime;

        if (!_isAnimationPlaying/*|| m_swordCollision.m_enableMesh == false*/)
        {
            #region --- attack stuff ---
            //m_attackNumber = Random.Range(0, 1);

            // 3 different attacks | Left Mouse | Right Mouse | Middle (On controller L1 R1 R2)
            // Hitting from the left
            //if (m_attackNumber == 0)
            //{
            #endregion
            if (m_attackCD <= 0)
            {
                //Debug.Log("test");
                m_swordCollision.m_enableMesh = true;
                m_attackCD = m_MaxAttackCD;
                // first animation played
                m_animation.Play("Hit");
            }
            // if player is doing nothing and animation is not playing
            else if (!_isAnimationPlaying)
            {
                m_swordCollision.m_enableMesh = false;
                m_animation.Play("Idle");
            }
        }
        #region --- second attack ---
        //// Hitting from right
        //else if (m_attackNumber == 1)
        //{
        //    m_swordCollision.m_enableMesh = true;
        //    // second animation played
        //    m_animation.Play("SwordTest");
        #endregion
    }

    void Block(bool _isAnimationPlaying)
    {
        //Blocking Stuff
        if (!_isAnimationPlaying)
        {
            #region --- blocking stuff ---
            //m_blockNumber = Random.Range(0, 1);
            //if (m_blockNumber == 0)
            //{
            //Blocking Stuff 1
            #endregion
            m_isBlocking = 1;
            #region --- blocking stuff 2 ---
            //}
            //if (m_blockNumber == 1)
            //{
            //    // Blocking stuff 2
            //}
            #endregion
        }
        else
        {
            m_isBlocking = 0;
        }
    }

    bool IsActionAnimationPlaying()
    {
        if (m_animation.IsPlaying("Block") ||
           m_animation.IsPlaying("Blockidle") ||
           m_animation.IsPlaying("Dash") ||
           m_animation.IsPlaying("Hit") ||
           m_animation.IsPlaying("Jump"))
        {
            return true;
        }
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            //Debug.Log("Enemy got hit");
            if (m_isBlocking == 0)
            {
                m_player.m_swordCollision.m_enableMesh = false;
                m_Hearths -= 2;
            }
        }
    }
}
