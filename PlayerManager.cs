using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region -- Singleton --

    // Singleton
    // Player manager
    public static PlayerManager m_instance;
    void Awake()
    {
        m_instance = this;
    }

    #endregion

    /// <summary>
    /// Variables of the player manager
    /// </summary>
    // References the player
    public GameObject m_player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
