using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    public RayCollider caster;
    public float speedModifier = 1.0f;
    [SerializeField]
    private float xSpeed = -2;
    [SerializeField]
    private float ySpeed = -2;
    [SerializeField]
    private float minDistance = 0f;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        xSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * speedModifier;
        ySpeed = Input.GetAxis("Vertical") * Time.deltaTime * speedModifier;
        caster.ControlShowOff(xSpeed, ySpeed);




    }
    void FixedUpdate()
    {
        move(xSpeed, ySpeed);
    }

    void move(float xSpeed,float ySpeed)
    {
        int counter = 1;
        bool xOrY;
        foreach (RaycastHit2D[] element in caster.CastAllDirection(xSpeed, ySpeed))
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
                        if(element[i].distance < Mathf.Abs(clamp))
                        {
                            if(clamp > 0)
                            {
                                clamp = (element[i].distance-minDistance);
                            }
                            else if (clamp < 0)
                            {
                                clamp = -(element[i].distance-minDistance);
                            }
                        }
                        
                    }
                    else
                    {
                        
                    }
                }
                if(pass == true)
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
