using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theWall : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private Transform tf;
    
    private Transform spriteDirection;
    // Start is called before the first frame update
    void Awake()
    {
        tf = transform;

        //Find players automatic in scene
        if(GameObject.FindGameObjectWithTag("Player1") != null)
          player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        if (GameObject.FindGameObjectWithTag("Player2") != null)
            player2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player1 != null && player2 != null)
        {
            Debug.DrawLine(player1.position, player1.position, new Color(1.0f, 1.0f, 1.0f));
            tf.position = (player1.position - player2.position) / 2 + player2.position;

            tf.LookAt(player1);
            tf.rotation = tf.rotation * Quaternion.Euler(0, 270, 0);

            //NOT OPTIMAL!!! MAYBE DO NOT USE LOOKAT() FROM UNITY
            if (tf.eulerAngles.y != -180 && tf.eulerAngles.y != 180 && tf.eulerAngles.y != 180)
            {
                tf.eulerAngles = new Vector3(tf.eulerAngles.x, 0, tf.eulerAngles.z);
            }
        }
       


        //Debug.Log(tf.position);
    }
}
