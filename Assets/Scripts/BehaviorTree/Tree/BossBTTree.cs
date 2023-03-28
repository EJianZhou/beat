using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTtry.AI;

public class BossBTTree : BehavioTree
{

    public bool b = true;
    protected override void Start()
    {
        base.Start();


        BTSequentialNode bTSequentialNode0 = new BTSequentialNode();

        BTWaitNode bTWaitNode1 = new BTWaitNode(10);
        BTSelectNode selectNode2 = new BTSelectNode();

        BTSequentialNode bTSequentialNode1 = new BTSequentialNode();
        BTSequentialNode bTSequentialNode2 = new BTSequentialNode();
        BTSequentialNode bTSequentialNode3 = new BTSequentialNode();

        FuncConditionNode funcConditionNode1 = new FuncConditionNode(IsClose);
        FuncConditionNode funcConditionNode2 = new FuncConditionNode(IsMidRange);
        FuncConditionNode funcConditionNode3 = new FuncConditionNode(IsAway);


        BossAttack1 bossAttack1 = new BossAttack1();
        bTSequentialNode1.Open(funcConditionNode1,bossAttack1);
        bTSequentialNode2.Open(funcConditionNode2,bossAttack1);
        bTSequentialNode3.Open(funcConditionNode3,bossAttack1);
        selectNode2.Open(bTSequentialNode1, bTSequentialNode2, bTSequentialNode3);


        bTSequentialNode0.Open(bTWaitNode1,selectNode2);
        AddNode(bTSequentialNode0);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool IsClose()
    {
        return b;
    }

    public bool IsMidRange()
    {
        return b;
    }
    public bool IsAway()
    {
        return b;
    }


}
