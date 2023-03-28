using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerState
{
    // Start is called before the first frame update
    public PlayerAttack2State(PlayerFSM fsm, PlayerController player) : base(fsm, player)
    {
        mStateID = PlayerStateID.Attack2;
    }

    public override void Act()
    {
        Debug.Log("CNMinit2");
        mPlayer.Attack2();
    }

    public override void Reason(PlayerStateID psid)
    {
        if (psid == PlayerStateID.Attack3) mFSM.PerformTransition(PlayerTransition.Attack2ToAttack3);
        if (psid == PlayerStateID.Idle) mFSM.PerformTransition(PlayerTransition.Attack2ToIdle);
        if (psid == PlayerStateID.Move) mFSM.PerformTransition(PlayerTransition.Attack2ToMove);
        if (psid == PlayerStateID.Defend) mFSM.PerformTransition(PlayerTransition.Attack2ToDefend);
    }
}
