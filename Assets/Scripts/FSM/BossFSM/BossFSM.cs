using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossFSM
{
    private List<BossState> States = new List<BossState>();

    protected BossState currentState;
    public BossState CurrentState { get { return currentState; } }


    public void refresh(Beats beats)
    {
        currentState.Act();
    }
    public void AddState(BossState state)
    {
        Debug.Log(state.StateID);
        if (state == null)
        {
            Debug.LogError("要添加的状态为空");
            return;
        }
        if (States.Count == 0)
        {
            States.Add(state);
            currentState = state;
            return;
        }
        foreach (BossState s in States)
        {
            if (s.StateID == state.StateID)
            {
                Debug.LogError("要添加的状态ID[" + s.StateID + "]已经添加");
                return;
            }
        }
        States.Add(state);
    }

    public void DelectState(BossStateID stateID)
    {
        if (stateID == BossStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (BossState s in States)
        {
            if (s.StateID == stateID)
            {
                States.Remove(s);
                return;
            }
        }
        Debug.LogError("要删除的StateID不存在集合中:" + stateID);
    }

    public void PerformTransition(BossTransition trans)
    {
        if (trans == BossTransition.NullTansition)
        {
            Debug.LogError("要执行的转换条件为空 ： " + trans); return;
        }
        BossStateID nextStateID = currentState.GetOutPutStateID(trans);
        if (nextStateID == BossStateID.NullState)
        {
            Debug.LogError("在转换条件 [" + trans + "] 下，没有对应的转换状态"); return;
        }
        foreach (BossState s in States)
        {
            if (s.StateID == nextStateID)
            {
                currentState.DoBeforeLeaving();
                currentState = s;
                currentState.DoBeforeEntering();
                return;
            }
        }
    }


}