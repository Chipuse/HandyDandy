using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    Rigidbody2D rb;
    public float additionalMinecartGravity;
    public bool isPushed = false;
    public bool physicControlled = true;

    public bool grounded;
    public RayCollider rayCollider;
    public LayerMask mask;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Mathf.Abs(rb.velocity.x) >= 0.01f && isPushed == false && physicControlled)
        {
            SoundControl.Instance.IncreaseFadeVolume("MineCartWheel", 0.3f);
            if(physicControlled)
                isPushed = true;
        }
        else if(isPushed && physicControlled && Mathf.Abs(rb.velocity.x) <= 0.01f)
        {
            SoundControl.Instance.IncreaseFadeVolume("MineCartWheel", -0.3f);
            isPushed = false;
        }
        else if (!physicControlled)
        {
            
        }


        if (!grounded)
        {
            //rb.AddForce(new Vector2(0.0f, additionalMinecartGravity * Time.deltaTime));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - additionalMinecartGravity * Time.deltaTime);
        }
    }
    public void Mute()
    {
        SoundControl.Instance.IncreaseFadeVolume("MineCartWheel", -0.3f);        
    }
    public void PlaySound()
    {
        SoundControl.Instance.IncreaseFadeVolume("MineCartWheel", +0.3f);
    }
    private void OnDestroy()
    {
        SoundControl.Instance.FadeVolume("MineCartWheel", 0.0f);
    }



    void FixedUpdate()
    {
        rayCollider.ControlShowOff(0, -1);
        rayCollider.debugRays(mask);
        grounded = false;
        
        bool pass = true;

        RaycastHit2D[] ground = rayCollider.checkDown(-1, mask);
        if (ground == null)
            grounded = false;
        else
        {
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                {
                    pass = false;                                       
                }
            }

            if (!pass)
            {
                grounded = true;
            }
        }


    }
}
