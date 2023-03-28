using System.Collections;
using System.Collections.Generic;
using Beatsgame;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private int flag = 1;
    public GameObject player1, player2;
    public PlayerController pc1, pc2;
    public enum op{
        NONE,
        STUNNED,
        SKILL1,
        SKILL2,
        SKILL3,
    }
    public enum status{
        END,
        START,
        ACTIVE,
        UNACTIVE,
    }
    public struct BossOp
    {
        public op operation;
        public status st;
    }
    int frame = 0;
    Dictionary<int,Queue<BossOp>> dic = new Dictionary<int, Queue<BossOp>>();
    BossOp nowstatus = new BossOp();


    public void Awake()
    {
        pc1 = player1.GetComponent<PlayerController>();
        pc2 = player2.GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    public void init()
    {
        nowstatus.operation = op.NONE;
        nowstatus.st = status.END;
    }
    public void bossupdate()
    {
        frame++;
        Dealwithqueue(frame);
        if(dic.ContainsKey(frame))dic.Remove(frame);
    }

    public void bossadd(BossOp op)
    {
        Debug.Log("攻击！111");
        switch(op.operation)
        {
            case BossManager.op.SKILL1:
                Debug.Log("攻击！222");
                if (flag == 1)
                {
                    ToolMgr.Instance.RingCount(1);
                    flag = 0;
                }
                real_update(frame+50,op,1,24,10);
                //TODO
                break;
        }
    }

    private void Dealwithqueue(int x)
    {
        if(dic.ContainsKey(x))
        while(dic.ContainsKey(x)&&dic[x].Count>0)
        {
            Debug.Log("攻击！333");
            BossOp op = dic[x].Dequeue();
            DealWithOp(op);
        }
    }

    private void DealWithOp(BossOp op)
    {
        switch(op.operation)
        {
            case BossManager.op.SKILL1:
                nowstatus.operation=BossManager.op.SKILL1;
                switch(op.st)
                {
                    case status.START:
                        nowstatus.st=status.START;
                        //EventMgr.Instance.Emit("bossIdle"+op.bossid.ToString(), null);
                        Debug.Log("攻击事件发送！");
                        EventMgr.Instance.Emit("BossAttack1", null);
                        break;
                    case status.ACTIVE:
                        pc1.bossSkill1 = 1;
                        pc2.bossSkill1 = 1;
                        nowstatus.st=status.ACTIVE;
                        break;
                    case status.UNACTIVE:
                        pc1.bossSkill1 = 0;
                        pc2.bossSkill1 = 0;
                        nowstatus.st=status.UNACTIVE;
                        break;
                    case status.END:
                        nowstatus.operation=BossManager.op.NONE;
                        nowstatus.st=status.END;
                        EventMgr.Instance.Emit("BossIdle", null);
                        break;
                }
                break;
        }
    }

    private void real_update(int x,BossOp op,int pre,int rem,int suf)
    {
        op.st=status.START;
        if(!dic.ContainsKey(x))
        {
            Queue<BossOp> q =new Queue<BossOp>();
            dic.Add(x,q);
        }
        dic[x].Enqueue(op);
        Debug.Log(x);
        op.st=status.ACTIVE;
        if(!dic.ContainsKey(x+pre))
        {
            Queue<BossOp> q =new Queue<BossOp>();
            dic.Add(x+pre,q);
        }
        dic[x+pre].Enqueue(op);
        op.st=status.UNACTIVE;
        if(!dic.ContainsKey(x+pre+rem))
        {
            Queue<BossOp> q =new Queue<BossOp>();
            dic.Add(x+pre+rem,q);
        }
        dic[x+pre+rem].Enqueue(op);
        op.st=status.END;
        if(!dic.ContainsKey(x+pre+rem+suf))
        {
            Queue<BossOp> q =new Queue<BossOp>();
            dic.Add(x+pre+rem+suf,q);
        }
        dic[x+pre+rem+suf].Enqueue(op);
        //Debug.Log("OPOPOPOPOPOP3 "+dic.ContainsKey(x+pre+rem+suf));
    }
}
