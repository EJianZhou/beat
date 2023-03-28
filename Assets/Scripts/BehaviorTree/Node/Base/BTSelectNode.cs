using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    //选择节点
    public class BTSelectNode : BTBifurcateNode
    {
        public override NodeState Tick()
        {
            Debug.Log("select");
            var childNode = children[currentChild].Tick();
            switch (childNode)
            {
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Failure:
                    currentChild++;
                    if (currentChild == children.Count)
                    {
                        currentChild = 0;
                        return NodeState.Failure;
                    }
                    return NodeState.Running;
                case NodeState.Running:
                    return NodeState.Running;
            }
            return NodeState.Success;
        }
    }

}