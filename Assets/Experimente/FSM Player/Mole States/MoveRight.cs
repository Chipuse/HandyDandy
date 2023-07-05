using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : IFSMState<FSMMole>
{
    public void Enter(FSMMole p)
    {
        p.sR.flipX = true;
        p.sRR.flipX = true;
        p.crystal.SetPositionAndRotation(p.crystRight.position, p.crystRight.rotation);
        p.crystal.localScale = p.crystRight.localScale;
    }

    public void Reason(FSMMole p)
    {
        if (p.checkPlayerUp() == false)
        {
            Debug.Log("changeStuck");
            p.ChangeState(new Stuck());
            Debug.Log("right to stuck");
            return;
        }
        bool pass = true;

        if(p.checkFloorRight() == false)
            pass = false;

        if (p.checkWallRight() == false)
            pass = false;
        if (p.checkPlayerRight() == false)
            pass = false;


        if (!pass)
        {
            if (p.checkFloorLeft() == false || p.checkWallLeft() == false || p.checkPlayerLeft() == false)
            {
                Debug.Log("changeStuck");
                p.ChangeState(new Stuck());
            }
            else
                p.ChangeState(new MoveLeft());
        }


    }
    public void Update(FSMMole p)
    {

    }
    public void FixedUpdate(FSMMole p)
    {
        
        p.move(p.xSpeed, p.ySpeed);
        p.rayCollider.ControlShowOff(p.xSpeed, p.groundCheck);
        p.rayCollider.debugRays(p.mask);

        p.rayCollider.ControlShowOff(0, p.wallCheck);
        p.rayCollider.debugRays(p.playerMask);
    }
    public void Exit(FSMMole p)
    {
        
    }
}
