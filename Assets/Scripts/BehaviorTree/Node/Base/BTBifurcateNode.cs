using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    
    public abstract class BTBifurcateNode : BTNode
    {
        protected List<BTNode> children = new List<BTNode>();
        protected int currentChild = 0;

        public virtual BTBifurcateNode Open(params BTNode[] childNode)
        {
            for (int i = 0; i < childNode.Length; i++)
            {
                children.Add(childNode[i]);
            }

            return this;
        }

    }
}