using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayCollider))]
public class FSMMole : StatefulMonoBehaviour<FSMMole>
{
    public LayerMask mask;

    public LayerMask playerMask;
    public RayCollider rayCollider;
    public float xSpeed = 1;
    float speedModifier = 100;
    float realxSpeed= 0;
    public float ySpeed = 0;
    float minDistance;

    public float groundCheck = -0.1f;
    public float wallCheck = 0.1f;
    public float playerCheck = 1;

    public Animator animator;
    public Animator animatorR;
    public SpriteRenderer sR;
    public SpriteRenderer sRR;
    public Transform crystal;

    public Transform crystLeft;
    public Transform crystRight;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        sR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rayCollider = GetComponent<RayCollider>();        
        fsm = new FSM<FSMMole>();
        fsm.Configure(this, new MoveRight());
        
    }

    public void move(float xSpeed, float ySpeed)
    {
        realxSpeed = xSpeed / speedModifier;
        rayCollider.ControlShowOff(realxSpeed, ySpeed);
        int counter = 1;
        bool xOrY;
        foreach (RaycastHit2D[] element in rayCollider.CastAllDirection(realxSpeed, ySpeed, mask))
        {
            if (counter == 1)
            {
                xOrY = true;
            }
            else if (counter == 2)
            {
                xOrY = true;
            }
            else if (counter == 3)
            {
                xOrY = false;
            }
            else
            {
                xOrY = false;
            }

            //print("counter");
            if (element != null)
            {
                bool pass = true;
                float clamp;
                if (xOrY)
                {
                    clamp = realxSpeed;
                }
                else
                {
                    clamp = ySpeed;
                }

                for (int i = 0; i < element.Length; i++)
                {
                    //print(i);
                    if (element[i].collider != null)
                    {
                        pass = false;
                        if (element[i].distance < Mathf.Abs(clamp))
                        {
                            if (clamp > 0)
                            {
                                clamp = (element[i].distance - minDistance);
                            }
                            else if (clamp < 0)
                            {
                                clamp = -(element[i].distance - minDistance);
                            }
                        }

                    }
                    else
                    {

                    }
                }
                if (pass == true)
                {
                    if (xOrY)
                    {
                        transform.Translate(new Vector3(realxSpeed, 0, 0));
                    }
                    else
                    {
                        transform.Translate(new Vector3(0, ySpeed, 0));
                    }
                }
                else
                {
                    //print(clamp);
                    if (xOrY)
                    {
                        transform.Translate(new Vector3(clamp, 0, 0));
                    }
                    else
                    {
                        transform.Translate(new Vector3(0, clamp, 0));
                    }

                }
                //print(counter);

            }
            counter++;
        }
    }
    
    public bool checkFloorRight()
    {
        bool pass = true;
        if (rayCollider.checkDown(groundCheck, mask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkDown(groundCheck, mask);
            if(ground[ground.Length-1].collider == null)            
                    pass = false;

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkFloorLeft()
    {
        bool pass = true;
        if (rayCollider.checkDown(groundCheck, mask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkDown(groundCheck, mask);
            if (ground[0].collider == null)
                pass = false;

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkWallRight()
    {
        bool pass = true;
        if (rayCollider.checkRight(wallCheck, mask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkRight(wallCheck, mask);
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                    pass = false;
            }

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkWallLeft()
    {
        bool pass = true;
        if (rayCollider.checkLeft(-wallCheck, mask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkLeft(-wallCheck, mask);
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                    pass = false;
            }

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkPlayerRight()
    {
        bool pass = true;
        if (rayCollider.checkRight(playerCheck, playerMask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkRight(playerCheck, playerMask);
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                    pass = false;
            }

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkPlayerLeft()
    {
        bool pass = true;
        if (rayCollider.checkLeft(-playerCheck, playerMask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkLeft(-playerCheck, playerMask);
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                    pass = false;
            }

        }
        else
        {
            pass = false;
        }
        return pass;
    }
    public bool checkPlayerUp()
    {
        bool pass = true;
        if (rayCollider.checkUp(wallCheck, playerMask) != null)
        {
            RaycastHit2D[] ground = rayCollider.checkUp(wallCheck, playerMask);
            foreach (RaycastHit2D element in ground)
            {
                if (element.collider != null)
                {
                    pass = false;                   
                }
            }

        }
        else
        {
            pass = false;
        }
        return pass;
    }
}
