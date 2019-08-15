using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchwertCollisionEnabler : MonoBehaviour
{

    //PlayerScript m_player;
    MeshCollider m_swordMesh;
    public bool m_enableMesh;

    // Use this for initialization
    void Start()
    {
        //m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        m_swordMesh = GetComponent<MeshCollider>();

        m_swordMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // checking mesh every frame
        ChangeMeshState();

        //Debug.Log(m_enableMesh);
    }

    /// <summary>
    /// changes the state of the mesh | swinging sword activates it / holding deactivates it
    /// </summary>
    void ChangeMeshState()
    {
        m_swordMesh.enabled = m_enableMesh;
    }
}
