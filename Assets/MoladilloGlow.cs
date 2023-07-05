using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoladilloGlow : MonoBehaviour
{
    private Animator _anim;

    private lamp _lamp;
    void Start()
    {
        _anim = GetComponent<Animator>();

        if (_lamp == null)
        {
            _lamp = GetComponentInChildren<lamp>();
        }
        
        _lamp.BorderEnter += EnableGlow;
        _lamp.BorderExit += DisableGlow;
    }


    void EnableGlow()
    {
        _anim.SetLayerWeight(1,1);
    }
    
    void DisableGlow()
    {
        _anim.SetLayerWeight(1,0);
    }
}
