using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    //顺序节点
    public class BTSequentialNode : BTBifurcateNode
    {
        public override NodeState Tick()
        {
            Debug.Log("sequential");
            var childNode = children[currentChild].Tick();
            switch (childNode)
            {
                case NodeState.Success:
                    currentChild++;
                    if (currentChild == children.Count)
                    {
                        currentChild = 0;
                    }
                    return NodeState.Success;
                case NodeState.Failure:
                    return NodeState.Failure;
                case NodeState.Running:
                    return NodeState.Running;
            }
            return NodeState.Success;
        }
    }

}