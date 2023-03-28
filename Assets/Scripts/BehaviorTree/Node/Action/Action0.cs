using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTtry.AI;

public class Action0 : BTActionNode
{
    public override NodeState Tick()
    {
        Debug.Log("动作一");
        return NodeState.Success;
    }
}
