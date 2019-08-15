using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Controller : MonoBehaviour
{
    public PlayerScript m_Player;
    public EnemyScript m_Enemy;

    //leben links
    public int m_lifeLeft = 6;
    //leben Rechts
    private int m_lifeRight = 6;
     

    // Use this for initialization
    void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        m_Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        m_lifeLeft = m_Player.m_MaxHearths;
        //Lebens abzug Linker Spieler

        m_lifeRight = m_Enemy.m_Hearths;
        //Lebens abzug Rechter Spieler

        //Nachfarage des lebens des Linken Spielers und löchung
        if (m_lifeLeft < 6)
        {
          Destroy(GameObject.Find("Heart_left"));
        }
        if (m_lifeLeft < 5)
        {
            Destroy(GameObject.Find("Heart_right"));
        }
        if (m_lifeLeft < 4)
        {
            Destroy(GameObject.Find("Heart_left (1)"));
        }
        if (m_lifeLeft < 3)
        {
            Destroy(GameObject.Find("Heart_right (1)"));
        }
        if (m_lifeLeft < 2)
        {
            Destroy(GameObject.Find("Heart_left (2)"));
        }
        if (m_lifeLeft < 1)
        {
            Destroy(GameObject.Find("Heart_right (2)"));
        }


        //Nachfarage des lebens des Rechten Spielers und löchung
        if (m_lifeRight < 6)
        {
            Destroy(GameObject.Find("Heart_leftRight"));
        }
        if (m_lifeRight < 5)
        {
            Destroy(GameObject.Find("Heart_rightRight"));
        }
        if (m_lifeRight < 4)
        {
            Destroy(GameObject.Find("Heart_leftRight (1)"));
        }
        if (m_lifeRight < 3)
        {
            Destroy(GameObject.Find("Heart_rightRight (1)"));
        }
        if (m_lifeRight < 2)
        {
            Destroy(GameObject.Find("Heart_leftRight (2)"));
        }
        if (m_lifeRight < 1)
        {
            Destroy(GameObject.Find("Heart_rightRight (2)"));
        }
    }
}
