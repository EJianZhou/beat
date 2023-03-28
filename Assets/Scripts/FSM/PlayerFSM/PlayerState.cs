using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态转换条件
public enum PlayerTransition
{
    NullTansition,

    IdleToMove,
    IdleToDefend,
    IdleToAttack1,
    IdleToHit,

    MoveToIdle,
    MoveToDefend,
    MoveToAttack1,
    MoveToHit,

    DefendToIdle,
    DefendToMove,
    DefendToAttack1,
    DefendToHit,

    Attack1ToIdle,
    Attack1ToAttack2,
    Attack1ToMove,
    Attack1ToDefend,
    Attack1ToHit,

    Attack2ToIdle,
    Attack2ToAttack3,
    Attack2ToMove,
    Attack2ToDefend,
    Attack2ToHit,

    Attack3ToIdle,
    Attack3ToAttack4,
    Attack3ToMove,
    Attack3ToDefend,
    Attack3ToHit,

    Attack4ToIdle,
    Attack4ToAttack1,
    Attack4ToMove,
    Attack4ToDefend,
    Attack4ToHit,

    HitToIdle,


}

//状态的ID
public enum PlayerStateID
{
    NullState,
    Idle,
    Move,
    Defend,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Hit,
}
/// <summary>
/// 此类表示有限状态系统中的状态。
/// </summary>
public abstract class PlayerState
{

    protected Dictionary<PlayerTransition, PlayerStateID> mMap = new Dictionary<PlayerTransition, PlayerStateID>();
    protected PlayerStateID mStateID;
    protected PlayerFSM mFSM;
    public PlayerController mPlayer;

    public PlayerStateID StateID { get { return mStateID; } }

    public PlayerState(PlayerFSM fsm, PlayerController player)
    {
        mFSM = fsm;
        mPlayer = player;
    }

    public void AddTransition(PlayerTransition trans, PlayerStateID id)
    {
        if (trans == PlayerTransition.NullTansition)
        {
            Debug.LogWarning("当前状态CubeTransiton为空 :" + trans);
            return;
        }

        if (id == PlayerStateID.NullState)
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

    public void DelectTransition(PlayerTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在");
            return;
        }
        mMap.Remove(trans);
    }

    public PlayerStateID GetOutPutStateID(PlayerTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogWarning("当前状态 :" + trans + " 的ID为空状态：" + mMap[trans]);
            return PlayerStateID.NullState;
        }
        else
        {
            return mMap[trans];
        }
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }

    public abstract void Reason(PlayerStateID psid);
    public abstract void Act();

}