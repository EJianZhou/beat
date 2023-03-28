using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack4State : PlayerState
{

    public PlayerAttack4State(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Attack4;
    }

    public override void Act()
    {
        Debug.Log("CNMinit4");
        mPlayer.Attack4();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid == PlayerStateID.Attack1) mFSM.PerformTransition(PlayerTransition.Attack4ToAttack1);
        if (psid == PlayerStateID.Idle) mFSM.PerformTransition(PlayerTransition.Attack4ToIdle);
        if (psid == PlayerStateID.Move) mFSM.PerformTransition(PlayerTransition.Attack4ToMove);
        if (psid == PlayerStateID.Defend) mFSM.PerformTransition(PlayerTransition.Attack4ToDefend);
    }
}
