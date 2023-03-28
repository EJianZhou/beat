using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(BossFSM fsm, BossController player) : base(fsm, player)
    {
        mStateID = BossStateID.Idle;
    }

    public override void Act()
    {
        mPlayer.Idle();
    }

    public override void Reason(BossStateID bsID)
    {
        if (bsID == BossStateID.Attack1) mFSM.PerformTransition(BossTransition.IdleToAttack1);
        if (bsID == BossStateID.Attack2) mFSM.PerformTransition(BossTransition.IdleToAttack2);
        if (bsID == BossStateID.Attack3) mFSM.PerformTransition(BossTransition.IdleToAttack3);
        if (bsID == BossStateID.Attack4) mFSM.PerformTransition(BossTransition.IdleToAttack4);
        if (bsID == BossStateID.Attack5) mFSM.PerformTransition(BossTransition.IdleToAttack5);
    }
}