using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCollider : MonoBehaviour
{
    public float offsetX = 10;
    public float offsetY = 10;
    public float originOffsetX = 0.0f;
    public float originOffsetY = 0.0f;
    public int castDensity = 2;

    public Vector3[] rayOriginsRight;
    public Vector3[] rayOriginsLeft;
    public Vector3[] rayOriginsUp;
    public Vector3[] rayOriginsDown;
    public bool showRays = true;


    public RaycastHit2D[] check;
    [SerializeField]
    private float xSpeed = 0;
    [SerializeField]
    private float ySpeed = 0;
    [SerializeField]
    private float tolerance = 0.001f;

    //to match a 2D box Collider on gameObject
    public BoxCollider2D box;
    public bool useBoxCollider = true;



    
    private void Awake()
    {
        if (useBoxCollider)
        {
            if(box == null)
            {
                box = GetComponent<BoxCollider2D>();
            }
            if(box != null)
            {
                originOffsetX = box.offset.x;
                originOffsetY = box.offset.y;
                offsetX = box.size.x / 2;
                offsetY = box.size.y / 2;
            }
        }
    }
    void Start()
    {
        setRays(castDensity);
        
    }

    
    void Update() {

        //debugRays();

        //ControlShowOff();
        /*
        foreach (RaycastHit2D element in checkRight(xSpeed))
        {
            if (element)
                print("hit");
            else
                print("miss");
        }
        */
        //CastAllDirection(xSpeed, ySpeed);

    }

    public void debugRays()
    {
        int counter = 1;
        Vector3[] actOrigins;
        Vector3 actOffset;
        foreach (RaycastHit2D[] element in CastAllDirection(xSpeed, ySpeed))
        {
            if (counter == 1)
            {
                actOrigins = rayOriginsRight;
                actOffset = new Vector3(xSpeed, 0, 0);
            }
            else if (counter == 2)
            {
                actOrigins = rayOriginsLeft;
                actOffset = new Vector3(xSpeed, 0, 0);
            }
            else if (counter == 3)
            {
                actOrigins = rayOriginsUp;
                actOffset = new Vector3(0, ySpeed, 0);
            }
            else
            {
                actOrigins = rayOriginsDown;
                actOffset = new Vector3(0, ySpeed, 0);
            }

            //print("counter");
            if (element != null)
            {
                for (int i = 0; i < element.Length; i++)
                {
                    //print(i);
                    if (element[i].collider != null)
                    {
                        Debug.DrawLine(transform.position + actOrigins[i], transform.position + actOrigins[i] + actOffset, Color.red, 0.0f);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position + actOrigins[i], transform.position + actOrigins[i] + actOffset, Color.green, 0.0f);
                    }
                }
            }
            counter++;
        }
    }

    public void debugRays(LayerMask mask)
    {
        int counter = 1;
        Vector3[] actOrigins;
        Vector3 actOffset;
        foreach (RaycastHit2D[] element in CastAllDirection(xSpeed, ySpeed, mask))
        {
            if (counter == 1)
            {
                actOrigins = rayOriginsRight;
                actOffset = new Vector3(xSpeed, 0, 0);
            }
            else if (counter == 2)
            {
                actOrigins = rayOriginsLeft;
                actOffset = new Vector3(xSpeed, 0, 0);
            }
            else if (counter == 3)
            {
                actOrigins = rayOriginsUp;
                actOffset = new Vector3(0, ySpeed, 0);
            }
            else
            {
                actOrigins = rayOriginsDown;
                actOffset = new Vector3(0, ySpeed, 0);
            }

            //print("counter");
            if (element != null)
            {
                for (int i = 0; i < element.Length; i++)
                {
                    //print(i);
                    if (element[i].collider != null)
                    {
                        Debug.DrawLine(transform.position + actOrigins[i], transform.position + actOrigins[i] + actOffset, Color.red, 0.0f);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position + actOrigins[i], transform.position + actOrigins[i] + actOffset, Color.green, 0.0f);
                    }
                }
            }
            counter++;
        }
    }

    //Creatng an 4 slot array containing the raycast results of all 4 directions
    public RaycastHit2D[][] CastAllDirection(float xSpeed,float ySpeed)
    {
        RaycastHit2D[][] result;
        /*
        RaycastHit2D[] arRayRight = checkRight(xSpeed);
        RaycastHit2D[] arRayLeft = checkLeft(xSpeed);
        RaycastHit2D[] arRayUp = checkUp(ySpeed);
        RaycastHit2D[] arRayDown = checkDown(ySpeed);
        */

        result = new RaycastHit2D[4][];
        result[0] = checkRight(xSpeed);
        result[1] = checkLeft(xSpeed);
        result[2] = checkUp(ySpeed);
        result[3] = checkDown(ySpeed);
        return result;
    }

    public RaycastHit2D[][] CastAllDirection(float xSpeed, float ySpeed, LayerMask mask)
    {
        RaycastHit2D[][] result;
        /*
        RaycastHit2D[] arRayRight = checkRight(xSpeed);
        RaycastHit2D[] arRayLeft = checkLeft(xSpeed);
        RaycastHit2D[] arRayUp = checkUp(ySpeed);
        RaycastHit2D[] arRayDown = checkDown(ySpeed);
        */

        result = new RaycastHit2D[4][];
        result[0] = checkRight(xSpeed, mask);
        result[1] = checkLeft(xSpeed, mask);
        result[2] = checkUp(ySpeed, mask);
        result[3] = checkDown(ySpeed, mask);
        return result;
    }



    //Shooting Rays right
    public RaycastHit2D[] checkRight(float xSpeed)
    {
        if (xSpeed <= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsRight.Length];
        for (int i = 0; i < rayOriginsRight.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsRight[i] + transform.position, Vector3.right, xSpeed);
        }

        return result;
    }
    public RaycastHit2D[] checkRight(float xSpeed, LayerMask mask)
    {
        if (xSpeed <= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsRight.Length];
        for (int i = 0; i < rayOriginsRight.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsRight[i] + transform.position, Vector3.right, xSpeed, mask);
        }

        return result;
    }

    //shooting rays Left
    public RaycastHit2D[] checkLeft(float xSpeed)
    {
        if (xSpeed >= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsLeft.Length];
        for (int i = 0; i < rayOriginsLeft.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsLeft[i] + transform.position, Vector3.left, -xSpeed);
        }

        return result;
    }
    public RaycastHit2D[] checkLeft(float xSpeed, LayerMask mask)
    {
        if (xSpeed >= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsLeft.Length];
        for (int i = 0; i < rayOriginsLeft.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsLeft[i] + transform.position, Vector3.left, -xSpeed, mask);
        }

        return result;
    }

    //shooting rays Up
    public RaycastHit2D[] checkUp(float ySpeed)
    {
        if (ySpeed <= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsUp.Length];
        for (int i = 0; i < rayOriginsUp.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsUp[i] + transform.position, Vector3.up, ySpeed);
        }

        return result;
    }
    public RaycastHit2D[] checkUp(float ySpeed, LayerMask mask)
    {
        if (ySpeed <= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsUp.Length];
        for (int i = 0; i < rayOriginsUp.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsUp[i] + transform.position, Vector3.up, ySpeed, mask);
        }

        return result;
    }

    //schooting rays  Down
    public RaycastHit2D[] checkDown(float ySpeed)
    {
        if (ySpeed >= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsDown.Length];
        for (int i = 0; i < rayOriginsDown.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsDown[i] + transform.position, Vector3.down, -ySpeed);
        }

        return result;
    }
    public RaycastHit2D[] checkDown(float ySpeed, LayerMask mask)
    {
        if (ySpeed >= 0)
        {
            return null;
        }
        RaycastHit2D[] result = new RaycastHit2D[rayOriginsDown.Length];
        for (int i = 0; i < rayOriginsDown.Length; i++)
        {
            result[i] = Physics2D.Raycast(rayOriginsDown[i] + transform.position, Vector3.down, -ySpeed, mask);
        }

        return result;
    }

    void setRays(int density)
    {
        float rayGapX = (offsetX * 2) / density;
        float rayGapY = (offsetY * 2) / density;
        //for the right side
        rayOriginsRight = new Vector3[density + 1];
        for (int i = 0; i < density + 1; i++)
        {
            if(i == 0)
            {
                rayOriginsRight[i] = new Vector3(+offsetX + originOffsetX, +offsetY - rayGapY * i - tolerance + originOffsetY, 0);
            }
            else if(i == density)
            {
                rayOriginsRight[i] = new Vector3(+offsetX + originOffsetX, +offsetY - rayGapY * i + tolerance + originOffsetY, 0);
            }
            else
                rayOriginsRight[i] = new Vector3(+offsetX + originOffsetX, +offsetY - rayGapY * i + originOffsetY, 0);
        }

        //for the left side
        rayOriginsLeft = new Vector3[density + 1];
        for (int i = 0; i < density + 1; i++)
        {
            if (i == 0)
            {
                rayOriginsLeft[i] = new Vector3(-offsetX + originOffsetX, +offsetY - rayGapY * i - tolerance + originOffsetY, 0);
            }
            else if (i == density)
            {
                rayOriginsLeft[i] = new Vector3(-offsetX + originOffsetX, +offsetY - rayGapY * i + tolerance + originOffsetY, 0);
            }
            else
                rayOriginsLeft[i] = new Vector3(-offsetX + originOffsetX, +offsetY - rayGapY * i + originOffsetY, 0);
        }

        //for the up side
        rayOriginsUp = new Vector3[density + 1];
        for (int i = 0; i < density + 1; i++)
        {
            if (i == 0)
            {
                rayOriginsUp[i] = new Vector3(-offsetX + rayGapX * i + tolerance + originOffsetX, +offsetY + originOffsetY, 0);
            }
            else if (i == density)
            {
                rayOriginsUp[i] = new Vector3(-offsetX + rayGapX * i - tolerance + originOffsetX, +offsetY + originOffsetY, 0);
            }
            else
                rayOriginsUp[i] = new Vector3(-offsetX + rayGapX * i + originOffsetX, +offsetY + originOffsetY, 0);
        }

        //for the down side
        rayOriginsDown = new Vector3[density + 1];
        for (int i = 0; i < density + 1; i++)
        {
            if (i == 0)
            {
                rayOriginsDown[i] = new Vector3(-offsetX + rayGapX * i + tolerance + originOffsetX, -offsetY + originOffsetY, 0);
            }
            else if (i == density)
            {
                rayOriginsDown[i] = new Vector3(-offsetX + rayGapX * i - tolerance + originOffsetX, -offsetY + originOffsetY, 0);
            }
            else
                rayOriginsDown[i] = new Vector3(-offsetX + rayGapX * i + originOffsetX, -offsetY + originOffsetY, 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(-offsetX + originOffsetX, +offsetY + originOffsetY, 0), transform.position + new Vector3(+offsetX + originOffsetX, +offsetY + originOffsetY, 0));
        Gizmos.DrawLine(transform.position + new Vector3(+offsetX + originOffsetX, +offsetY + originOffsetY, 0), transform.position + new Vector3(+offsetX + originOffsetX, -offsetY + originOffsetY, 0));
        Gizmos.DrawLine(transform.position + new Vector3(+offsetX + originOffsetX, -offsetY + originOffsetY, 0), transform.position + new Vector3(-offsetX + originOffsetX, -offsetY + originOffsetY, 0));
        Gizmos.DrawLine(transform.position + new Vector3(-offsetX + originOffsetX, -offsetY + originOffsetY, 0), transform.position + new Vector3(-offsetX + originOffsetX, +offsetY + originOffsetY, 0));


        float sphereSize = 0.05f;
        foreach (Vector3 element in rayOriginsRight)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(element + transform.position, sphereSize);
           

        }
        foreach (Vector3 element in rayOriginsLeft)
        {

            Gizmos.DrawSphere(element + transform.position, sphereSize);
        }
        foreach (Vector3 element in rayOriginsUp)
        {

            Gizmos.DrawSphere(element + transform.position, sphereSize);
        }
        foreach (Vector3 element in rayOriginsDown)
        {

            Gizmos.DrawSphere(element + transform.position, sphereSize);
        }


    }

    public void ControlShowOff()
    {
        xSpeed = Input.GetAxis("Horizontal");
        ySpeed = Input.GetAxis("Vertical");
    }
    public void ControlShowOff(float x,float y)
    {
        xSpeed = x;
        ySpeed = y;
    }
}