using System.Collections;
using System.Collections.Generic;
using Beatsgame;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public enum op{
        NONE,
        STUNNED,
        ATTACK,
        DEFEND,
        SKILL,
    }
    public enum status{
        END,
        START,
        ACTIVE,
        UNACTIVE,
    }
    public struct PlayerOp
    {
        public int playerid;
        public op operation;
        public status st;
        public int attid;
        public int attUAC;
    }
    int frame = 0;
    Dictionary<int,Queue<PlayerOp>> dic = new Dictionary<int, Queue<PlayerOp>>();
    PlayerOp nowstatus = new PlayerOp();
    // Start is called before the first frame update
    public void init()
    {
        nowstatus.operation = op.NONE;
        nowstatus.st = status.END;
        nowstatus.attid = 0;
    }
    public void playerupdate(PlayerOp op)
    {
        frame++;
        Dealwithqueue(frame);
        switch(op.operation)
        {
            case PlayerManager.op.ATTACK:
                if(nowstatus.operation == PlayerManager.op.NONE)
                {
                    op.attid = 1;
                    op.attUAC = frame+13+5;
                    real_update(frame,op,10,15,62-25);
                }
                else if(nowstatus.operation == PlayerManager.op.ATTACK)
                {
                    if(nowstatus.attid==4)//玩家在第四下攻击后又按了第五下，删除ring
                    {
                        Debug.Log("??????????????");
                        UIRoot.Instance.StopRingCount("Ring",0);
                        break;
                    }
                    if(nowstatus.st==status.START)
                    {
                        ;
                    }
                    else if(nowstatus.st==status.ACTIVE)
                    {
                        op.attid = nowstatus.attid+1;
                        int sta = nowstatus.attUAC;
                        op.attUAC = sta+13+5;
                        if (op.attid == 2)//第二下攻击放入队列
                        {
                            real_update(sta, op, 10, 15, 64-25);
                        }
                        else if (op.attid == 3)//第三下攻击放入队列
                        {
                            real_update(sta, op, 10, 15, 65-25);
                        }
                        else if(op.attid==4)//第四下攻击放入队列
                        {
                            real_update(sta,op,20,30,113-50);
                        }
                    }
                    else if(nowstatus.st==status.UNACTIVE)
                    {
                        op.attid = nowstatus.attid+1;
                        op.attUAC = frame+13+5;
                        if(op.attid==4)//第四下攻击放入队列
                        {
                            real_update(frame,op,13,5,75);
                        }
                        else real_update(frame,op,13,5,23);
                    }
                }
                //TODO
                break;
            case PlayerManager.op.DEFEND:
                if(nowstatus.operation == PlayerManager.op.NONE)
                {
                    real_update(frame,op,1,24,38-25);
                }
                break;
            
        }
        Dealwithqueue(frame);
        if(dic.ContainsKey(frame))dic.Remove(frame);
    }
    private void Dealwithqueue(int x)
    {
        if(dic.ContainsKey(x))
        while(dic.ContainsKey(x)&&dic[x].Count>0)
        {
            PlayerOp op = dic[x].Dequeue();
            if(nowstatus.operation==PlayerManager.op.ATTACK)
            {
                if(op.attid<nowstatus.attid)continue;
            }
            DealWithOp(op);
        }
    }

    private void DealWithOp(PlayerOp op)
    {
        Debug.Log("NOW"+frame+" DEAL:"+op.operation.ToString()+" "+op.st.ToString()+" "+op.attid.ToString());
        switch(op.operation)
        {
            case PlayerManager.op.ATTACK:
                nowstatus.operation=PlayerManager.op.ATTACK;
                nowstatus.attid=op.attid;
                nowstatus.attUAC=op.attUAC;
                switch(op.st)
                {
                    case status.START:
                        nowstatus.st=status.START;
                        //EventMgr.Instance.Emit("PlayerIdle"+op.playerid.ToString(), null);
                        Debug.Log("DIUPlayerAttack"+op.attid.ToString()+op.playerid.ToString());
                        EventMgr.Instance.Emit("PlayerAttack"+op.attid.ToString()+op.playerid.ToString(), null);
                        if(op.attid==4)
                        {
                            ToolMgr.Instance.RingCount(1);//第四下攻击开始处理，1Beat
                        }
                        break;
                    case status.ACTIVE:
                        nowstatus.st=status.ACTIVE;
                        break;
                    case status.UNACTIVE:
                        nowstatus.st=status.UNACTIVE;
                        break;
                    case status.END:
                        nowstatus.operation=PlayerManager.op.NONE;
                        nowstatus.st=status.END;
                        EventMgr.Instance.Emit("PlayerIdle"+op.playerid.ToString(), null);
                        break;
                }
                break;
            case PlayerManager.op.DEFEND:
                
                nowstatus.operation=PlayerManager.op.DEFEND;
                switch(op.st)
                {
                    case status.START:
                        Debug.Log("OPOPOPOPOPOP2 "+"PlayerDefend"+op.playerid.ToString());
                        nowstatus.st=status.START;
                        EventMgr.Instance.Emit("PlayerDefend"+op.playerid.ToString(), null);
                        break;
                    case status.ACTIVE:
                        nowstatus.st=status.ACTIVE;
                        break;
                    case status.UNACTIVE:
                        nowstatus.st=status.UNACTIVE;
                        break;
                    case status.END:
                        nowstatus.operation=PlayerManager.op.NONE;
                        nowstatus.st=status.END;
                        EventMgr.Instance.Emit("PlayerIdle"+op.playerid.ToString(), null);
                        break;
                }
                break;
        }
    }

    private void real_update(int x,PlayerOp op,int pre,int rem,int suf)
    {
        op.st=status.START;
        if(!dic.ContainsKey(x))
        {
            Queue<PlayerOp> q =new Queue<PlayerOp>();
            dic.Add(x,q);
        }
        dic[x].Enqueue(op);
        Debug.Log(x);
        op.st=status.ACTIVE;
        if(!dic.ContainsKey(x+pre))
        {
            Queue<PlayerOp> q =new Queue<PlayerOp>();
            dic.Add(x+pre,q);
        }
        dic[x+pre].Enqueue(op);
        op.st=status.UNACTIVE;
        if(!dic.ContainsKey(x+pre+rem))
        {
            Queue<PlayerOp> q =new Queue<PlayerOp>();
            dic.Add(x+pre+rem,q);
        }
        dic[x+pre+rem].Enqueue(op);
        op.st=status.END;
        if(!dic.ContainsKey(x+pre+rem+suf))
        {
            Queue<PlayerOp> q =new Queue<PlayerOp>();
            dic.Add(x+pre+rem+suf,q);
        }
        dic[x+pre+rem+suf].Enqueue(op);
        //Debug.Log("OPOPOPOPOPOP3 "+dic.ContainsKey(x+pre+rem+suf));
    }
}
