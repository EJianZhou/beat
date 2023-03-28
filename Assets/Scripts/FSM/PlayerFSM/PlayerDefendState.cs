using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefendState : PlayerState
{
    public PlayerDefendState(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Defend;
    }

    public override void Act()
    {
        Debug.Log("!!!");

        mPlayer.Defend();
    }

    public override void Reason(PlayerStateID psid)
    {
        mPlayer.LastDefend();
        if (psid == PlayerStateID.Idle)
        {
            mFSM.PerformTransition(PlayerTransition.DefendToIdle);
        }
        else if (psid == PlayerStateID.Move)
        {
            mFSM.PerformTransition(PlayerTransition.DefendToMove);
        }
        else if (psid==PlayerStateID.Attack1)
        {
            Debug.Log("CNM转了Attack1");
            mFSM.PerformTransition(PlayerTransition.DefendToAttack1);
        }
    }
}