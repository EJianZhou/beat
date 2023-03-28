using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态转换条件
public enum BossTransition
{
    NullTansition,

    Attack1ToIdle,
    Attack2ToIdle,
    Attack3ToIdle,
    Attack4ToIdle,
    Attack5ToIdle,

    IdleToAttack1,
    IdleToAttack2,
    IdleToAttack3,
    IdleToAttack4,
    IdleToAttack5

}

//状态的ID
public enum BossStateID
{
    NullState,
    Idle,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5
}
/// <summary>
/// 此类表示有限状态系统中的状态。
/// </summary>
public abstract class BossState
{

    protected Dictionary<BossTransition, BossStateID> mMap = new Dictionary<BossTransition, BossStateID>();
    protected BossStateID mStateID;
    protected BossFSM mFSM;
    public BossController mPlayer;

    public BossStateID StateID { get { return mStateID; } }

    public BossState(BossFSM fsm, BossController player)
    {
        mFSM = fsm;
        mPlayer = player;
    }

    public void AddTransition(BossTransition trans, BossStateID id)
    {
        if (trans == BossTransition.NullTansition)
        {
            Debug.LogWarning("当前状态CubeTransiton为空 :" + trans);
            return;
        }

        if (id == BossStateID.NullState)
        {
            Debug.LogWarning("当前状态ID : CubeStateID为空：" + id);
            return;
        }

        if (mMap.ContainsKey(trans))
        {
            Debug.LogWarning("当前状态：" + trans + " 已经添加过了");
            return;
        }
        mMap.Add(trans, id);
    }

    public void DelectTransition(BossTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在");
            return;
        }
        mMap.Remove(trans);
    }

    public BossStateID GetOutPutStateID(BossTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogWarning("当前状态 :" + trans + " 的ID为空状态：" + mMap[trans]);
            return BossStateID.NullState;
        }
        else
        {
            return mMap[trans];
        }
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }

    public abstract void Reason(BossStateID bsID);
    public abstract void Act();

}