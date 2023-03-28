using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTtry.AI;
using System;

public class FuncConditionNode : BTConditionNode
{
    private Func<bool> m_function;
    public FuncConditionNode(Func<bool> func)
    {
        this.m_function = func;
    }
    public override NodeState Tick()
    {
        Debug.Log("condition");
        if (m_function!=null)
        {
            return (m_function.Invoke()) ? NodeState.Success : NodeState.Failure;
        }
        return NodeState.Failure;
    }


}
