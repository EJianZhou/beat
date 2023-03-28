using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossManager;
public class BossSkill1 : MonoBehaviour
{
    private static BossSkill1 _instance;
    public static BossSkill1 Instance
    {
        get
        {
            return _instance;
        }
    }
    public struct Skill1Beats
    {
        public int type;
        public int interval;
    };

    int length;
    int nowpos;
    int flag;
    Skill1Beats[] Beat = new Skill1Beats[100];
    int startpos;
    int intervalp;

    BossManager bm;

    public void StartCount(int len,Skill1Beats[] beats)
    {
        Debug.Log("HELLO?");
        length = len;
        Beat = beats;
        nowpos=0;
        flag=0;
        intervalp = 40;
        startpos = -160;
        EventMgr.Instance.AddListener("BEAT!",CreateBox);
    }

    void CreateBox(string event_name, object udata)
    {
        EventMgr.Instance.RemoveListener("BEAT!",CreateBox);
        UIRoot.Instance.init("Prefabs","Box","FunctionLayer",0);
        EventMgr.Instance.AddListener("BEAT!",CreateBeat);
    }

    void CreateBeat(string event_name, object udata)
    {
        flag++;
        if(flag == Beat[nowpos].interval)
        {
            flag = 0;
            startpos += intervalp*Beat[nowpos].interval;
            Vector3 v = new Vector3(startpos+462.5f,140+198.0f,0);
            UIRoot.Instance.init("Prefabs","DefendBeat","FunctionLayer",nowpos);
            UIRoot.Instance.MoveSon("FunctionLayer","DefendBeat",nowpos, "Point",v);
            nowpos++;
            if(nowpos == length)
            {
                Debug.Log("HELLOHELLO");
                flag = 0;
                nowpos = 0;
                EventMgr.Instance.RemoveListener("BEAT!",CreateBeat);
                ToolMgr.Instance.wait(5,StartAttack);
            }
        }
    }



    public void StartAttack(string event_name, object udata)
    {
        EventMgr.Instance.AddListener("BEAT!",Attack);
        EventMgr.Instance.RemoveListener("BEAT!",StartAttack);
        UIRoot.Instance.uninit("Box",0);
        for(int i = 0;i<length;i++)
        {
            UIRoot.Instance.uninit("DefendBeat",i);
        }
    }

    void Attack(string event_name, object udata)
    {
        flag++;
        if(flag == Beat[nowpos].interval)
        {
            flag = 0;
            //
            BossOp op  = new BossOp();
            op.operation =BossManager.op.SKILL1;
            bm.bossadd(op);
            nowpos++;
            if(nowpos == length)
            {
                flag = 0;
                nowpos = 0;
                EventMgr.Instance.RemoveListener("BEAT!",Attack);
            }
        }
    }

    void Awake() {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        bm = this.gameObject.GetComponent<BossManager>();
    }
    
}
