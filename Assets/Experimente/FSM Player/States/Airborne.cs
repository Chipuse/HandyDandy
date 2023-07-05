using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : IFSMState<FSMPlayerControl>
{
    

    public void Enter(FSMPlayerControl p)
    {
        //p.ySpeed *= 1.5f;
    }

    public void Reason(FSMPlayerControl p)
    {
        bool pass = true;
        RaycastHit2D[] ground = p.rayCollider.checkDown(p.ySpeed, p.mask);
        foreach(RaycastHit2D element in ground)
        {
            if(element.collider != null)
                pass = false;
        }
        
        if (!pass)
        {
            p.ChangeState(new Airborne());
        }
            
    }
    public void Update(FSMPlayerControl p)
    {
        p.xSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
    }
    public void FixedUpdate(FSMPlayerControl p)
    {
        p.move(p.xSpeed,p.ySpeed);
    }
    public void Exit(FSMPlayerControl p)
    {

    }
}
