using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTtry.AI
{
    public abstract class BehavioTree:MonoBehaviour
    {
        protected BTRootNode mRoot;
        protected virtual void Start()
        {
            mRoot = new BTRootNode();
        }

        protected virtual void Update()
        {
            mRoot.Tick();
        }

        protected void AddNode(params BTNode[] child)
        {
            mRoot.Open(child);
        }
    }
}

