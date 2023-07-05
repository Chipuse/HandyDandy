using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChanger : MonoBehaviour
{

    public GameObject[] jumps;
    private int active = 1;
    public Vector3 pos;
     
    public void Start()
    {
        setActive(0);
    }
    
   public void setActive(int input)
    {
        foreach (GameObject item in jumps)
        {
            item.SetActive(false);
        }
        jumps[input].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            pos = jumps[active - 1].transform.position;
            active = 1;
            setActive(active - 1);
            jumps[active - 1].transform.position = pos;
        }
        else if (Input.GetKeyDown("2"))
        {
            pos = jumps[active - 1].transform.position;
            active = 2;
            setActive(active - 1);
            jumps[active - 1].transform.position = pos;
        }
        else if (Input.GetKeyDown("3"))
        {
            pos = jumps[active - 1].transform.position;
            active = 3;
            setActive(active - 1);
            jumps[active - 1].transform.position = pos;
        }
        else if (Input.GetKeyDown("4"))
        {
            pos = jumps[active - 1].transform.position;
            active = 4;
            setActive(active - 1);
            jumps[active - 1].transform.position = pos;
        }
        else if (Input.GetKeyDown("5"))
        {
            pos = jumps[active - 1].transform.position;
            active = 5;
            setActive(active - 1);
            jumps[active - 1].transform.position = pos;
        }
    }
}
