using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(RayCollider))]
public class simpleControl : MonoBehaviour
{
    string input;
    string jumpButton = "empty";
    string movementAxis = "empty";


    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public int player = 1;
    private int notCollideLayer;
    public BoxCollider2D boxCollider;
    public BoxCollider2D groundCollider;
    public Transform groundCheck;
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_Grounded;
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;

    public LayerMask mask;
    public LayerMask wallsMask;
    public RayCollider rayCollider;
    public float groundDistance;
    public float maxFallSpeed = 0;
    public float gravityThreshold = 0;
    public float peakGravity = 0;
    private float normalGravity = 0;
    private bool isRising = false;

    public GameObject dustH;
    public GameObject dustD;
    public GameObject dustHLand;
    public GameObject dustDLand;
    public Transform dustPosition;

    private bool dusty;



    // Start is called before the first frame update
    void Start()
    {
        getControl();
    }
    private void Awake()
    {
        rayCollider = GetComponent<RayCollider>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (player == 1)
        {
            notCollideLayer = 12;
        }
        else
        {
            notCollideLayer = 13;
        }

        normalGravity = m_Rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rayCollider.ControlShowOff(0, -groundDistance);
        rayCollider.debugRays(mask);
        m_Grounded = false;
        dusty = false;
        bool pass = true;

        RaycastHit2D[] ground = rayCollider.checkDown(-groundDistance, mask);
        if (ground == null)
            m_Grounded = false;
        else
        {
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                {
                    pass = false;
                    if (element.collider.gameObject.layer == 10 || element.collider.gameObject.layer == 11)
                        dusty = true;
                }



            }

            if (!pass)
            {
                if (!dusty)
                {
                    if (m_Rigidbody2D.velocity.y <= -10)
                    {
                        if (player == 1)
                            MakeDust(dustDLand);
                        else if (player == 2)
                            MakeDust(dustHLand);
                    }
                    else if (m_Rigidbody2D.velocity.y <= -1)
                    {
                        if (player == 1)
                            MakeDust(dustD);
                        else if (player == 2)
                            MakeDust(dustH);
                    }
                }

                m_Grounded = true;

            }
        }


    }

    void Update()
    {
        if (input == "empty")
        {
            getControl();
        }
        if (player == 1)
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (CrossPlatformInputManager.GetAxis(movementAxis) != 0 && movementAxis != "empty")
            {
                h = CrossPlatformInputManager.GetAxis(movementAxis);
            }
            if (m_Grounded)
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown(jumpButton))
                {
                    //MakeDustDandy();
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    isRising = true;
                }

            }
            if (h > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (h < 0)
            {
                spriteRenderer.flipX = true;
            }
            if (CrossPlatformInputManager.GetButton("Jump") || CrossPlatformInputManager.GetButton(jumpButton))
            {

            }
            else if (isRising && m_Rigidbody2D.velocity.y > gravityThreshold)
            {
                isRising = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, gravityThreshold);
            }

            move(h);
            animator.SetFloat("FallSpeed", m_Rigidbody2D.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(h));
        }
        else if (player == 2)
        {
            float h = CrossPlatformInputManager.GetAxis("HorizontalP2");
            if (CrossPlatformInputManager.GetAxis(movementAxis) != 0 && movementAxis != "empty")
            {
                h = CrossPlatformInputManager.GetAxis(movementAxis);
            }
            if (m_Grounded)
            {
                if (CrossPlatformInputManager.GetButtonDown("JumpP2") || CrossPlatformInputManager.GetButtonDown(jumpButton))
                {
                    //MakeDustHandy();
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    isRising = true;
                }

            }
            if (h > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (h < 0)
            {
                spriteRenderer.flipX = true;
            }
            if (CrossPlatformInputManager.GetButton("JumpP2") || CrossPlatformInputManager.GetButton(jumpButton))
            {

            }
            else if (isRising && m_Rigidbody2D.velocity.y > gravityThreshold)
            {
                isRising = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, gravityThreshold);
            }
            move(h);
            animator.SetFloat("FallSpeed", m_Rigidbody2D.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(h));
        }
        animator.SetBool("Grounded", m_Grounded);



        if (maxFallSpeed != 0)
            if (m_Rigidbody2D.velocity.y <= -maxFallSpeed)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -maxFallSpeed);
            }
        if (gravityThreshold != 0 && peakGravity != 0)
            if (Mathf.Abs(m_Rigidbody2D.velocity.y) <= gravityThreshold)
            {
                m_Rigidbody2D.gravityScale = peakGravity;
            }
            else
            {
                m_Rigidbody2D.gravityScale = normalGravity;
            }


    }
    void MakeDust(GameObject dust)
    {
        Instantiate(dust, dustPosition.position, dust.transform.rotation);
    }

    void MakeDustLandDandy()
    {
        Instantiate(dustDLand, dustPosition.position, dustDLand.transform.rotation);
    }
    void MakeDustLandHandy()
    {
        Instantiate(dustHLand, dustPosition.position, dustHLand.transform.rotation);
    }

    void MakeDustDandy()
    {
        Instantiate(dustD, dustPosition.position, dustD.transform.rotation);
    }
    void MakeDustHandy()
    {
        Instantiate(dustH, dustPosition.position, dustH.transform.rotation);
    }


    void move(float move)
    {
        RaycastHit2D[] wall;
        if (move > 0)
        {
            wall = rayCollider.checkRight(move, wallsMask);
        }
        else if(move < 0)
        {
            wall = rayCollider.checkLeft(move, wallsMask);
        }
        
        //m_Rigidbody2D.AddForce(new Vector2(move, 0));
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

    }

    void getControl()
    {


        if (player == 1)
            input = InputManagment.getPlayerOne();
        if (player == 2)
            input = InputManagment.getPlayerTwo();

        if (input != "empty")
        {
            jumpButton = "Jump1" + input;
            movementAxis = "axis1" + input;
        }

    }

}
