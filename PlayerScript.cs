using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// max hp player
    /// </summary>
    public int m_MaxHearths = 3;

    /// <summary>
    /// max attack
    /// </summary>
    public float m_MaxAttackCD;
    float m_attackCD = 0f;

    bool m_Dash;

    // Attacking/Blocking left and right
    bool m_isAnimationPlaying;
    // 0 = nothing | left = 1 | right = 2
    int m_isBlocking = 0;
    //int m_isAttacking = 0;

    // Movement variables
    public float m_RotationSpeed;
    public float m_MoveSpeed;
    public float m_JumpPower;
    float m_ySpeed;

    // lock the camera angle
    public float m_MaxLookAngleUp;
    public float m_MaxLookAngleDown;

    // controls movement of the player
    CharacterController m_charController;

    // Camera attached to player
    Camera m_camera;

    // sword of character
    public GameObject m_sword;

    // shield of character
    GameObject m_shield;

    // collision enabler of character
    public SchwertCollisionEnabler m_swordCollision;

    //
    //DashController m_dashController;

    //
    EnemyScript m_enemy;

    //
    //Rigidbody m_rigid;
    //Animator m_animator;
    Animation m_animation;

    //
    bool m_DashisUp;

    // Dash
    Rigidbody m_rigidbody;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_rigidbody = GetComponent<Rigidbody>();

        //m_dashController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DashController>();

        m_camera = transform.Find("Main Camera").GetComponent<Camera>();
        m_charController = GetComponent<CharacterController>();
        m_enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();

        // looking for sword in child objects and get the variable to en- and disable the Mesh Filter of the sword
        //m_sword = transform.Find("Katana_Fixed").gameObject;
        m_swordCollision = m_sword.GetComponent<SchwertCollisionEnabler>();

        //m_rigid = GetComponent<Rigidbody>();
        //m_animator = GetComponentInChildren<Animator>();
        m_animation = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MaxHearths < 0)
            m_MaxHearths = 0;

        // Checks if a block or attack animation is playing
        m_isAnimationPlaying = IsActionAnimationPlaying();

        //DirectionFacing();

        // Movement of player
        ProcessMovement(m_isAnimationPlaying);

        // Player can only attack if he isnt already attacking or blocking
        Attack(m_isAnimationPlaying);
        Block(m_isAnimationPlaying);
    }

    /// <summary>
    /// Player attacks and swings his sword
    /// </summary>
    void Attack(bool _isAnimationPlaying)
    {
        m_attackCD -= Time.deltaTime;

        if (!_isAnimationPlaying && m_attackCD <= 0/* || m_swordCollision.m_enableMesh == false*/)
        {
            // 3 different attacks | Left Mouse | Right Mouse | Middle (On controller L1 R1 R2)
            // Attack with L1 (Hitting from left)
            if (!Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_swordCollision.m_enableMesh = true;
                m_attackCD = m_MaxAttackCD;
                // first animation played
                m_animation.Play("Hit");
            }
            #region --- ---
            //
            // Attack with R1 (Hitting from right)
            //else if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.F))
            //{
            //    m_swordCollision.m_enableMesh = true;
            //    // second animation played
            //    m_sword.GetComponent<Animation>().Play("SwordSwing2");
            //}
            // if player is doing nothing and animation is not playing
#endregion
            else if (!_isAnimationPlaying)
            {
                m_swordCollision.m_enableMesh = false;
            }
        }
    }

    void Block(bool _isAnimationPlaying)
    {
        if (!_isAnimationPlaying)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Blocking left
                    //Debug.Log("Links block");
                    m_isBlocking = 1;
                    m_animation.Play("Block");
                }
                #region --- asdf ---
                ////m_shield.GetComponent<Animation>().Play("Block1");
                //else if (Input.GetMouseButtonDown(1))
                //{
                //    //Blocking right
                //    Debug.Log("Rechtsblock");
                //    m_isBlocking = 2;
                //}
                #endregion
            }
            else
            {
                m_isBlocking = 0;
            }
        }
    }

    /// <summary>
    /// Controls the movement of the player
    /// </summary>
    void ProcessMovement(bool _isAnimationPlaying)
    {
        // rotate with mouse and rotation speed
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * m_RotationSpeed);

        m_camera.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * m_RotationSpeed);
        float angleForCamFor = Vector3.Angle(transform.forward, m_camera.transform.forward);
        float angleUpCamFor = Vector3.Angle(transform.up, m_camera.transform.forward);

        // Camera schaut zu weit, wir wissen noch nicht wohin genau
        if (angleForCamFor > Mathf.Min(m_MaxLookAngleUp, m_MaxLookAngleDown))
        {
            // nach oben
            if (angleUpCamFor < 90 && angleForCamFor > m_MaxLookAngleUp)
            {
                m_camera.transform.forward = transform.forward;
                m_camera.transform.Rotate(Vector3.right, -m_MaxLookAngleUp);
            }
            // nach unten
            else if (angleUpCamFor > 90 && angleForCamFor > m_MaxLookAngleDown)
            {
                m_camera.transform.forward = transform.forward;
                m_camera.transform.Rotate(Vector3.right, m_MaxLookAngleDown);
            }
        }

        if (!_isAnimationPlaying)
        {
            //Play Walk animation
            if ((Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0) ||
                (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0) ||
                (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") != 0))
            {
                m_animation.Play("Run");
            }
            else
            {
                m_animation.Play("Idle");
            }
            // Bewegen in 4 Richtungen
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            // normalisierung wichtig, da sonst eine Laufgeschwindigkeit von sqrt(2) möglich wäre
            move = move.normalized;
            move *= Time.deltaTime * m_MoveSpeed;

            // Wandelt eine Koordinate so um, dass sie vom Lokalspace aus berechnet wird
            move = transform.TransformDirection(move);

            // Ist true wenn wir den Boden berühren
            if (m_charController.isGrounded)
            {
                #region --- hier sollte ein dash sein ---
                //m_DashisUp = m_dashController.m_Dashjust1;

                /// <summary>
                /// dash with button "E"
                /// </summary>
                //if (Input.GetButton("Roll") && move.sqrMagnitude > 0.0f && m_DashisUp)
                //{
                //    m_rigidbody.AddForce(transform.forward * 10000);
                ////    //Dash();
                ////    //move *= 100000;                    
                //}

                //if(Input.GetKeyDown(KeyCode.Q) && m_DashisUp)
                //{
                //    //m_rigidbody.AddForce(Vector3.left * 1000);
                //    move *= 100;
                //}

#endregion

                // Springen mit Leertaste
                if (Input.GetButton("Jump"))
                {
                    m_ySpeed = m_JumpPower;
                    m_animation.Play("Jump");
                }
                else
                {
                    m_ySpeed = 0f;
                }
            }
            else
            {
                m_ySpeed -= 9.81f * Time.deltaTime;
            }

            move.y += m_ySpeed;

            m_charController.Move(move);
        }
    }

    #region --- noch mehr dash zeug
    //void DirectionFacing()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //        m_direction = 1;
    //    else if (Input.GetKeyDown(KeyCode.D))
    //        m_direction = 2;
    //    else if (Input.GetKeyDown(KeyCode.W))
    //        m_direction = 3;
    //    else if (Input.GetKeyDown(KeyCode.S))
    //        m_direction = 4;
    //}

    //void Dash()
    //{
    //    if (m_dashTime <= 0)
    //    {
    //        m_dashTime = m_startDashTime;
    //        rb.velocity = Vector3.zero;
    //    }
    //    else
    //    {
    //        m_dashTime -= Time.deltaTime;
    //
    //        Debug.Log("Dash müsste passieren");
    //        Debug.Log(m_direction);
    //
    //        if (m_direction == 1)
    //            rb.velocity = Vector3.left * m_dashSpeed;
    //        if (m_direction == 2)
    //            rb.velocity = Vector3.right * m_dashSpeed;
    //        if (m_direction == 3)
    //            rb.velocity = Vector3.up * m_dashSpeed;
    //        if (m_direction == 4)
    //            rb.velocity = Vector3.down * m_dashSpeed;
    //    }
    //}
#endregion

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
        if (other.gameObject.tag == "SwordEnemy")
        {
            if (m_isBlocking == 1)
            {
                //Kein Damage
            }
            else
            {
                m_enemy.m_swordCollision.m_enableMesh = false; 
                m_MaxHearths -= 2;
            }

        }
    }
}