using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    public enum NodeState
    {
        Success,
        Failure,
        Running
    }


    public abstract class BTNode
    {
        public abstract NodeState Tick();

    }

    public abstract class BTActionNode:BTNode
    {

    }



    public abstract class BTConditionNode:BTNode
    {

    }
}