using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : IFSMState<FSMMole>
{
    public void Enter(FSMMole p)
    {
        p.sR.flipX = false;
        p.sRR.flipX = false;
        p.crystal.SetPositionAndRotation(p.crystLeft.position,p.crystLeft.rotation);
        p.crystal.localScale = p.crystLeft.localScale;
        
        

    }

    public void Reason(FSMMole p)
    {
        if (p.checkPlayerUp() == false)
        {
            Debug.Log("changeStuck");
            p.ChangeState(new Stuck());
            Debug.Log("left to stuck");
            return;
        }
        bool pass = true;
        

        if (p.checkFloorLeft() == false)
            pass = false;

        if (p.checkWallLeft() == false)
            pass = false;

        if (p.checkPlayerLeft() == false)
            pass = false;


        if (!pass)
        {
            if (p.checkFloorRight() == false || p.checkWallRight() == false || p.checkPlayerRight() == false)
            {
                Debug.Log("changeStuck");
                p.ChangeState(new Stuck());
            }
            else
                p.ChangeState(new MoveRight());
            
        }

    }
    public void Update(FSMMole p)
    {
        
    }
    public void FixedUpdate(FSMMole p)
    {
        
        p.move(-p.xSpeed, p.ySpeed);
        p.rayCollider.ControlShowOff(-p.xSpeed, p.groundCheck);
        p.rayCollider.debugRays(p.mask);
        p.rayCollider.ControlShowOff(0, p.wallCheck);
        p.rayCollider.debugRays(p.playerMask);
    }
    public void Exit(FSMMole p)
    {
        
    }
}
