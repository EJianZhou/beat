using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    public class BTRootNode : BTBifurcateNode
    {
        public bool isStop = false;
        public override NodeState Tick()
        {
            Debug.Log("Root");
            var childNode = children[currentChild].Tick();
            while (true)
            {
                switch (childNode)
                {
                    case NodeState.Running:
                        return NodeState.Running;
                    default:
                        currentChild++;
                        if (currentChild == children.Count)
                        {
                            currentChild = 0;
                            return NodeState.Success;
                        }
                        continue;
                }
            }

        }
    }
}