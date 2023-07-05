using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(RayCollider))]
public class FSMPlayerControl : StatefulMonoBehaviour<FSMPlayerControl>
{
    public LayerMask mask;
    public RayCollider rayCollider;
    public float xSpeed;
    public float ySpeed;


    [SerializeField]
    private float minDistance = 0f;


    void Awake()
    {
        rayCollider = GetComponent<RayCollider>();
        xSpeed = 0;
        ySpeed = -0.05f;
        fsm = new FSM<FSMPlayerControl>();
        fsm.Configure(this, new Airborne());
    }
    
    public void move(float xSpeed, float ySpeed)
    {
        rayCollider.ControlShowOff(xSpeed, ySpeed);
        int counter = 1;
        bool xOrY;
        foreach (RaycastHit2D[] element in rayCollider.CastAllDirection(xSpeed, ySpeed, mask))
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
                    clamp = xSpeed;
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
                        transform.Translate(new Vector3(xSpeed, 0, 0));
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
}
