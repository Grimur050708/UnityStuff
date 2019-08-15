using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public Text m_HPBar;
    public Image m_Stamina;
    public PlayerScript m_Player;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        m_HPBar.text = m_Player.m_MaxHearths.ToString();
        //m_Stamina.rectTransform.localScale = new Vector3(m_Player.m_Stamina, 1, 1);
    }
}
