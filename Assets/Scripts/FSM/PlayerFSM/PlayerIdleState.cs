using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Idle;
    }

    public override void Act()
    {
        mPlayer.Idle();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid == PlayerStateID.Defend)
        {
            mFSM.PerformTransition(PlayerTransition.IdleToDefend);
        }
        else if (psid == PlayerStateID.Move)
        {
            mFSM.PerformTransition(PlayerTransition.IdleToMove);
        }
        else if (psid==PlayerStateID.Attack1)
        {
            Debug.Log("CNM转了Attack1");
            mFSM.PerformTransition(PlayerTransition.IdleToAttack1);
        }
    }
}