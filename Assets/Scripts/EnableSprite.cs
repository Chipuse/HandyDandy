using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSprite : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private lamp _lamp;
    
    // Start is called before the first frame update
    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
       
        if (_lamp == null)
        {
           _lamp = GetComponentInParent<lamp>();

        }

        _lamp.BorderEnter += EnableRender;
        _lamp.BorderExit += DisableRender;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableRender()
    {
        _SpriteRenderer.enabled = true;
    }

    private void DisableRender()
    {
        _SpriteRenderer.enabled = false;
    }
    
}
