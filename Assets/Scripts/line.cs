using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int count = 0;
    private float time;
    public float width;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 2f;
        lineRenderer.endWidth = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        lineRenderer.startWidth = Mathf.Sin(time*Mathf.PI)+width;
        lineRenderer.endWidth = Mathf.Cos(time*Mathf.PI)+width;
            

    }
}
