using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTtry.AI;

public class Action1 : BTActionNode
{
    public override NodeState Tick()
    {
        Debug.Log("动作二");
        return NodeState.Success;
    }
}
