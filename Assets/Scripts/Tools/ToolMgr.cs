using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventMgr;

public class ToolMgr : MonoBehaviour
{
    private static ToolMgr _instance;
    public static ToolMgr Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    public int FloatTransInt(float x,int y)
    {
        int ten = 1;
        for(int i=0;i<y;i++)ten = ten * 10;
        int ret = (int)(x*ten);
        return ret;
    }

    public float IntTransFloat(int x,int y)
    {
        int ten = 1;
        for(int i=0;i<y;i++)ten = ten * 10;
        float ret = ((float)x)/ten;
        return ret;
    }

    int waittime=0;
    int flag = 0;
    event_handler h;
    public void wait(int x,event_handler eh)
    {
        waittime = x;
        h=eh;
        flag=0;
        EventMgr.Instance.AddListener("BEAT!",Count);
    }

    void Count(string event_name, object udata)
    {
        flag++;
        if(flag==waittime)
        {
            EventMgr.Instance.RemoveListener("BEAT!",Count);
            EventMgr.Instance.AddListener("BEAT!",h);
        }
    }

    int ringtime=2;
    public void RingCount(int x)
    {
        ringtime = x;
        EventMgr.Instance.AddListener("BEAT!",initRing);
        
    
    }
    private void initRing(string event_name,object udata)
    {
        EventMgr.Instance.RemoveListener("BEAT!",initRing);
        UIRoot.Instance.init("Prefabs","Ring","FunctionLayer",0);
    }
    public int GetRingTime()
    {
        return ringtime;
    }
}
