using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2State : BossState
{
    public BossAttack2State(BossFSM fsm, BossController player) : base(fsm, player)
    {
        mStateID = BossStateID.Attack2;
    }

    public override void Act()
    {
        mPlayer.Attack2();
        //mAttackTimer += Time.deltaTime;
        //if (mAttackTimer > mAttackTime)
        //{
        //mPlayer.Attack(targets[0]);
        //mAttackTimer = 0;
        //}
    }

    public override void Reason(BossStateID bsID)
    {
        if (bsID==BossStateID.Idle) mFSM.PerformTransition(BossTransition.Attack2ToIdle);
    }



}