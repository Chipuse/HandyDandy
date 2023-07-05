using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{

    //get array list with buttons or make list with all buttons tags
    public static GameObject[][] list; // 2D array for Pointer
    public static GameObject[] list0;
    public static GameObject[] list1;
    public GameObject Pointer;
    int pInX = 0; //Array
    int pInY = 0; //
    int list0Length;


    void Start()
    {
        list = new GameObject[2][];
        list0 = GameObject.FindGameObjectsWithTag("Button");
        list1 = GameObject.FindGameObjectsWithTag("Button1");
     
        list[0] = list0;
        list[1] = list1;

        list0Length = list.GetLength(0);
        Debug.Log(list0Length);

        Pointer = list[pInX][pInY];
    }

    void Update()
    {
    
        
        if (Input.GetKeyUp("s") || Input.GetKeyUp("down"))
        {
            if (pInY < list0Length)
            {
                pInY++;
                Debug.Log(pInX +" "+ pInY);
            }
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            if(pInY > 0)
            {
                pInY--;
                Debug.Log(pInX + " " + pInY);
            }
        }

        if (Input.GetButtonUp("Submit") && pInY == 2)
       
        {
          if(pInX == 0)
            {
                pInY = 0;
                pInX++;
            }
          else if(pInX == 1)
            {
                pInY = 0;
                pInX--;
            }
        }

        if (Input.GetButtonUp("Submit"))
        {
            //Button button = list[pInX][pInX];
           // button.Press();
        }
        
        
        
        
        
        //onpresskey move in array to next/last index
        //onpresskey on selected button use buttons function
    }
}
