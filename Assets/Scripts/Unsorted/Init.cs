using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Init : MonoBehaviour
{
    public UnityEvent Action;

    private float ratio;

    private float m_ratio = 16f/9f;
    // Start is called before the first frame update
    void Start()
    {


    ratio = Screen.width / Screen.height;

    if (ratio < m_ratio)
    {
        int height = (int) (Screen.width / m_ratio);
        Screen.SetResolution(Screen.width, height, true);
    }
    else if (ratio > m_ratio)
    {
        int width = (int) (Screen.height * m_ratio);
        Screen.SetResolution(width, Screen.height, true);
    }

        Action.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
