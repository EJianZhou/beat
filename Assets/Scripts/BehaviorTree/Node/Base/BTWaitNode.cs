using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTtry.AI;
using System;

public class BTWaitNode : BTNode
{
    private int beatVal, num = 0,iffirst=1;
    public BTWaitNode(int beatVal)
    {
        this.beatVal = beatVal;
        this.iffirst = 1;
    }
    public override NodeState Tick()
    {
        if (iffirst==1)
        {
            EventMgr.Instance.AddListener("BEAT!", ValUpdate);
            iffirst = 0;
        }
        Debug.Log("puz" + num);
        if (num == beatVal)
        {
            num = 0;
            EventMgr.Instance.RemoveListener("BEAT!", ValUpdate);
            iffirst = 1;
            return NodeState.Success;
        }
        return NodeState.Running;
    }

    public void ValUpdate(string event_name, object udata)
    {
        num++;
    }


}
