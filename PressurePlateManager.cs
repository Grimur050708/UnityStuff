using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    #region -- Singleton --

    // Singleton
    // Player manager
    public static PressurePlateManager m_instancePlate;
    void Awake()
    {
        m_instancePlate = this;
    }

    #endregion

    /// <summary>
    /// Variables of the player manager
    /// </summary>
    // References the player
    public GameObject m_pressurePlate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
