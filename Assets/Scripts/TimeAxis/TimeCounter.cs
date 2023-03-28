using System;
using System.Collections;
using System.Collections.Generic;
using Beatsgame;
using UnityEngine;

public enum ObjectType
{
    Player,
    Boss,
}
[Serializable]
public struct ObjectComAttr
{
    public GameObject obj;
    public ObjectType obType;
}


public class TimeCounter : MonoBehaviour
{
    public ObjectComAttr[] refs=null;
    // Start is called before the first frame update
    private static TimeCounter _instance;
    public static TimeCounter Instance
    {
        get
        {
            return _instance;
        }
    }

    PlayerManager[] playerDic = new PlayerManager[100];
    BossManager[] bossDic = new BossManager[100];

    void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        for(int i=0;i<refs.Length;i++)
        {
            GameObject obj = refs[i].obj;
            if(refs[i].obType == ObjectType.Player)
            {
                PlayerManager pm = obj.GetComponent<PlayerManager>();
                playerDic[i] = pm;
            }
            else if(refs[i].obType == ObjectType.Boss)
            {
                BossManager bm = obj.GetComponent<BossManager>();
                bossDic[i] = bm;
            }
        }

        EventMgr.Instance.AddListener("ROperation",doupdate);
    }

    void doupdate(string event_name, object udata)
    {
        
        PlayerManager.PlayerOp[] ops = new PlayerManager.PlayerOp[refs.Length+1];
        for(int i=0;i<refs.Length;i++)
        {
            ops[i].operation=PlayerManager.op.NONE;
        }
        PlayerManager.PlayerOp newop = new PlayerManager.PlayerOp();
        ROperation replyop = (ROperation)udata;
        int len = replyop.Operations.Count;
        for(int i = 0;i < len;i++)
        {
            OP op = replyop.Operations[i];
            if(ops[op.Id].operation!=PlayerManager.op.NONE)continue;
            switch(op.Do)
            {
                case Command.PushJ:
                    newop = new PlayerManager.PlayerOp();
                    newop.operation = PlayerManager.op.ATTACK;
                    newop.playerid=op.Id;
                    ops[op.Id]=newop;
                    break;
                case Command.PushK:
                    Debug.Log("FIRSTSTEP!!!");
                    newop = new PlayerManager.PlayerOp();
                    newop.operation = PlayerManager.op.DEFEND;
                    newop.playerid=op.Id;
                    ops[op.Id]=newop;
                    break;
            }
        }
        for(int i=0;i<refs.Length;i++)
        {
            GameObject obj = refs[i].obj;
            if(refs[i].obType == ObjectType.Player)
            {
                Debug.Log("OPOPOPOP"+i.ToString());
                PlayerManager pm = playerDic[i];
                Debug.Log(111);
                pm.playerupdate(ops[i]);
            }
            else if(refs[i].obType == ObjectType.Boss)
            {
                BossManager bm = bossDic[i];
                bm.bossupdate();
            }
        }
    }

    
}
