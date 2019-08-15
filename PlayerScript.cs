using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// Variables of the player
    /// </summary>
    // Walk speed of the player
    public float m_WalkSpeed = 7.5f;
    // Rotation speed of the player
    public float m_RotationSpeed = 120f;
    
    /// <summary>
    /// Lock camera angles
    /// </summary>
    // Y axis up
    public float m_MaxLookAngleUp = 30;
    // Y axis down
    public float m_MaxLookAngleDown = 30;

    // Character controller
    CharacterController m_charContr;
    // Camera as a child of the player
    private Camera m_Camera;

    // Use this for initialization
    void Start()
    {
        // Get character controller
        m_charContr = GetComponent<CharacterController>();

        // Get camera as a child of the player
        m_Camera = GetComponentInChildren<Camera>();
               
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        // Rotation y axis
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * m_RotationSpeed);

        m_Camera.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * m_RotationSpeed);
        float angleForCamFor = Vector3.Angle(transform.forward, m_Camera.transform.forward);
        float angleUpCamFor = Vector3.Angle(transform.up, m_Camera.transform.forward);

        // Locks the angle of the camera (y axis)
        if (angleForCamFor > Mathf.Min(m_MaxLookAngleUp, m_MaxLookAngleDown))
        {
            // Up
            if (angleUpCamFor < 90 && angleForCamFor > m_MaxLookAngleUp)
            {
                m_Camera.transform.forward = transform.forward;
                m_Camera.transform.Rotate(Vector3.right, -m_MaxLookAngleUp);
            }
            // Down
            else if (angleUpCamFor > 90 && angleForCamFor > m_MaxLookAngleDown)
            {
                m_Camera.transform.forward = transform.forward;
                m_Camera.transform.Rotate(Vector3.right, m_MaxLookAngleDown);
            }
        }

        // Player movement in 4 directions
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // Normalizing of the walk speed
        move = move.normalized;
        move *= Time.deltaTime * m_WalkSpeed;
        
        // Converts coordinate
        move = transform.TransformDirection(move);
        
        m_charContr.Move(move);
    }
}