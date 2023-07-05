using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BugControl : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public int player = 1;
    private int notCollideLayer;
    public BoxCollider2D boxCollider;
    public BoxCollider2D groundCollider;
    public Transform groundCheck;
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_Grounded;          // Amount of force added when the player jumps.
    private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    private LayerMask m_WhatIsGround;
    // Whether or not the player is grounded.

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;


    public Vector3 oldPos;
    public int direction = 1;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (player == 1)
        {
            notCollideLayer = 12;
        }
        else
        {
            notCollideLayer = 13;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(oldPos == groundCheck.position)
        {
            direction = direction * -1;
        }
        m_Grounded = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(groundCheck.position, groundCollider.size, 0);


        foreach (Collider2D hit in hits)
        {
            if (hit == groundCollider || hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(groundCollider);

            if (colliderDistance.isOverlapped && hit.gameObject.layer != notCollideLayer && hit.gameObject.layer < 14)
            {
                //transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90)
                {
                    m_Grounded = true;
                }
            }
        }




        float h = m_MaxSpeed* direction;
            if (CrossPlatformInputManager.GetButtonDown("Jump") && m_Grounded)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            if (h > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (h < 0)
            {
                spriteRenderer.flipX = true;
            }
            move(h);
            animator.SetFloat("FallSpeed", m_Rigidbody2D.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(h));
        
        

        animator.SetBool("Grounded", m_Grounded);

        oldPos = groundCheck.position;
    }
    void move(float move)
    {
        //m_Rigidbody2D.AddForce(new Vector2(move, 0));
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

    }
}
