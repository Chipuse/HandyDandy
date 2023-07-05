using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    
    private float startPosX;
    private float startPosY;
    private float startPosZ;

    private float startValueX;
    private float startValueY;

    [SerializeField] private bool xMov;
    [SerializeField] private bool yMov = true;
    [SerializeField] private bool random = true;

    [SerializeField] private float stepSizeX = 0f;
    [SerializeField] private  float stepSizeY = 0.02f;

    [SerializeField] private float multiY = 0.08f;
    [SerializeField] private float multiX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startValueY = 0f;
        startPosY = transform.localPosition.y;

        startValueX = 0f;
        startPosX = transform.localPosition.x;

        startPosZ = transform.localPosition.z;

        if (random)
        {
            float random = Random.Range(-Mathf.PI, Mathf.PI);
            startValueY = startValueY + random;
            startValueX = startValueX + random;

        }

        //Debug.Log(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    { 
        
        transform.localPosition = new Vector3(startPosX + Mathf.Sin(startValueX) * multiX, startPosY +  Mathf.Sin(startValueY) * multiY, startPosZ + Mathf.Cos(startValueX) * multiX);
        //Mathf.Sin();
        
        if (yMov)
        {
         startValueY += stepSizeY;   
        }

        if (xMov)
        {
          startValueX += stepSizeX;  
        }
    }
}
