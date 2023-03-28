using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerState
{
    
    public PlayerAttack3State(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Attack3;
    }

    public override void Act()
    {
        Debug.Log("CNMinit3");
        mPlayer.Attack3();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid == PlayerStateID.Attack4) mFSM.PerformTransition(PlayerTransition.Attack3ToAttack4);
        if (psid == PlayerStateID.Idle) mFSM.PerformTransition(PlayerTransition.Attack3ToIdle);
        if (psid == PlayerStateID.Move) mFSM.PerformTransition(PlayerTransition.Attack3ToMove);
        if (psid == PlayerStateID.Defend) mFSM.PerformTransition(PlayerTransition.Attack3ToDefend);
    }
}
