using System.Collections;
using System.Collections.Generic;
using Beatsgame;
using UnityEngine;
using static BossSkill1;

public class Beats : MonoBehaviour
{
    public int frameNum = 0;
    private int firsttime = 0;
    public int flag = 1;
    private static Beats _instance;
    public Animator manimator;
    public static Beats Instance
    {
        get
        {
            return _instance;
        }
    }
    public int count = 0;

    int status = 0;
    void Awake() {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject warn1P1,warn2P1,warnBGP1;
    public GameObject warn1P2,warn2P2,warnBGP2;
    public bool w1p1,w2p1,w3p1;
    public bool w1p2,w2p2,w3p2;
    // Start is called before the first frame update
    void Start()
    {
        warn1P1.SetActive(false);
        warn2P1.SetActive(false);
        warnBGP1.SetActive(false);
        warn1P2.SetActive(false);
        warn2P2.SetActive(false);
        warnBGP2.SetActive(false);
        EventMgr.Instance.AddListener("ROperation",BEATS);
    }

    private void Update()
    {
        warn1P1.SetActive(w1p1);
        warn2P1.SetActive(w2p1);
        warnBGP1.SetActive(w3p1);
        warn1P2.SetActive(w1p2);
        warn2P2.SetActive(w2p2);
        warnBGP2.SetActive(w3p2);
    }

    void BEATS(string event_name, object udata)
    {
        frameNum++;
        if (frameNum == 150) SoundPlayerController.Instance.playBgmId = 1;
        // 50 frames/s
        // 0.02 s
        /*if(flag == 4)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }*/
        count++;
        if (count==7)
        {
            if (flag == 4)
            {
                //EventMgr.Instance.Emit("BossAttack", null);   
                flag = 0;
            }
            else flag++;
        }
        if (count == 17 && flag == 1)
        {
            //EventMgr.Instance.Emit("BossIdle", null);
        }
        if (count == 20)
        {
            //if (flag == 0) PlayerController.Instance.takeDamage();
        }
        if(count == 5)
        {
            //if (flag == 0) PlayerController.Instance.dealDamage();
        }
        if (count == 25)
        {
            if (flag == 2)
            {
                if(IDMgr.Instance.get_id()==1)w3p1 = true;
                else if(IDMgr.Instance.get_id()==2)w3p2 = true;
            }
            else if (flag == 3)
            {
                if(IDMgr.Instance.get_id()==1)w1p1 = true;
                else if(IDMgr.Instance.get_id()==2)w1p2 = true;
            }
            else if (flag == 4)
            {
                if(IDMgr.Instance.get_id()==1)
                {
                    w1p1 = false;
                    w2p1 = true;
                }
                else if(IDMgr.Instance.get_id()==2)
                {
                    w1p2 = false;
                    w2p2 = true;
                }
            }
            else if(flag == 0)
            {
                if(IDMgr.Instance.get_id()==1)
                {
                    w2p1 = false;
                    w3p1 = false;
                }
                else if(IDMgr.Instance.get_id()==2)
                {
                    w2p2 = false;
                    w3p2 = false;
                }
            }
        }
        if (count > 25)
        {
            ROperation rop = (ROperation)udata;
            EventMgr.Instance.Emit("BEAT!",rop.Frame);
            count = 1;
        }
    }
}
