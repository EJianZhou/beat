using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1State : BossState
{
    public BossAttack1State(BossFSM fsm, BossController player) : base(fsm, player)
    {
        mStateID = BossStateID.Attack1;
    }

    public override void Act()
    {
        mPlayer.Attack1();
        //mAttackTimer += Time.deltaTime;
        //if (mAttackTimer > mAttackTime)
        //{
        //mPlayer.Attack(targets[0]);
        //mAttackTimer = 0;
        //}
    }

    public override void Reason(BossStateID bsID)
    {
        if (bsID==BossStateID.Idle) mFSM.PerformTransition(BossTransition.Attack1ToIdle);
    }



}