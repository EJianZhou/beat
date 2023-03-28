using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1State : PlayerState
{
    public PlayerAttack1State(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Attack1;
    }

    public override void Act()
    {
        Debug.Log("CNMinit1");
        mPlayer.Attack1();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid == PlayerStateID.Attack2) mFSM.PerformTransition(PlayerTransition.Attack1ToAttack2);
        if (psid == PlayerStateID.Idle) mFSM.PerformTransition(PlayerTransition.Attack1ToIdle);
        if (psid == PlayerStateID.Move) mFSM.PerformTransition(PlayerTransition.Attack1ToMove);
        if (psid == PlayerStateID.Defend) mFSM.PerformTransition(PlayerTransition.Attack1ToDefend);
    }
}