using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuck : IFSMState<FSMMole>
{
    public void Enter(FSMMole p)
    {
        
        p.animator.SetTrigger("Stop");
        p.animatorR.SetTrigger("Stop");
    }

    public void Reason(FSMMole p)
    {
        if (p.checkPlayerUp() == true)
        {
            if (p.checkFloorLeft() == true && p.checkWallLeft() == true && p.checkPlayerLeft() == true)
            {
                //Debug.Log("changeLeft");
                p.ChangeState(new MoveLeft());
            }
            else if (p.checkFloorRight() == true && p.checkWallRight() == true && p.checkPlayerRight() == true)
            {
                //Debug.Log("changeRight");
                p.ChangeState(new MoveRight());
            }
        }
        


    }
    public void Update(FSMMole p)
    {

    }
    public void FixedUpdate(FSMMole p)
    {
        p.move(0.0f, p.ySpeed);
        p.rayCollider.ControlShowOff(0, p.wallCheck);
        p.rayCollider.debugRays(p.playerMask);

    }
    public void Exit(FSMMole p)
    {
        
        p.animator.SetTrigger("Go");
        p.animatorR.SetTrigger("Go");
    }
}