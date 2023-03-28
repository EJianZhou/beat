using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Move;
    }


    public override void Act()
    {
        mPlayer.Move();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid==PlayerStateID.Defend)
        {
            mFSM.PerformTransition(PlayerTransition.MoveToDefend);
        }
        else if (psid== PlayerStateID.Idle)
        {
            mFSM.PerformTransition(PlayerTransition.MoveToIdle);
        }
        else if (psid==PlayerStateID.Attack1)
        {
            mFSM.PerformTransition(PlayerTransition.MoveToAttack1);
        }
    }



}