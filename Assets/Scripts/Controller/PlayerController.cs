using System;
using System.Collections;
using System.Collections.Generic;
using Beatsgame;
using UnityEngine;
using FixMath.NET;
using static BossSkill1;

public class PlayerController : MonoBehaviour
{
    public GameObject obj1, obj2;
    public int bossSkill1 = 0;
    int moveflag = 1;
    public int id;
    public GameObject obj;
    public float speed = 1;
    float time = 0;
    public float x, y, z;
    Vector3 direction;
    bool K, W, A, S, D;
    bool underAttack = false;
    private PlayerFSM mFsmSystem;
    private Animator manimstor;
    int xv = 0, yv = 0;
    int ixv = 0, iyv = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 pos = this.gameObject.transform.position;
        x = pos.x;
        y = pos.y;
        z = pos.z;
        direction = new Vector3(0, 0, 1);
        MakeFSM();
        manimstor = GetComponent<Animator>();
        EventMgr.Instance.AddListener("PlayerMove" + id.ToString(), ToMove);
        EventMgr.Instance.AddListener("PlayerIdle" + id.ToString(), ToIdle);
        EventMgr.Instance.AddListener("PlayerDefend" + id.ToString(), ToDefend);
        EventMgr.Instance.AddListener("PlayerMoveToIdle" + id.ToString(), MoveToIdle);
        EventMgr.Instance.AddListener("PlayerAttack1" + id.ToString(), ToAttack1);
        EventMgr.Instance.AddListener("PlayerAttack2" + id.ToString(), ToAttack2);
        EventMgr.Instance.AddListener("PlayerAttack3" + id.ToString(), ToAttack3);
        EventMgr.Instance.AddListener("PlayerAttack4" + id.ToString(), ToAttack4);
        EventMgr.Instance.AddListener("ROperation", CharacterMove);

    }


    public void Hit()
    {

    }
    void ToMove(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Idle) mFsmSystem.CurrentState.Reason(PlayerStateID.Move);
    }

    void ToIdle(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Defend)  mFsmSystem.CurrentState.Reason(PlayerStateID.Idle); 
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack1) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack2) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack3) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack4) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
    }

    void MoveToIdle(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Move)
        {
            mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
            Debug.Log("MoveToIdle");
        }
    }

    public void LastDefend()
    {

    }

    void ToDefend(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Idle) mFsmSystem.CurrentState.Reason(PlayerStateID.Defend);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Move) mFsmSystem.CurrentState.Reason(PlayerStateID.Defend);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack1) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack2) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack3) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack4) mFsmSystem.CurrentState.Reason(PlayerStateID.Idle);
    }

    void ToAttack1(string event_name, object udata)
    {
        Debug.Log("CNM");
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Idle) mFsmSystem.CurrentState.Reason(PlayerStateID.Attack1);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Move) mFsmSystem.CurrentState.Reason(PlayerStateID.Attack1);
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Defend) mFsmSystem.CurrentState.Reason(PlayerStateID.Attack1);
    }

    void ToAttack2(string event_name, object udata)
    {
        Debug.Log("DIUPlayerAttack2?");
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack1)
        {
            Debug.Log("CNMtry2");
            mFsmSystem.CurrentState.Reason(PlayerStateID.Attack2);
        }
    }

    void ToAttack3(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack2) mFsmSystem.CurrentState.Reason(PlayerStateID.Attack3);
    }

    void ToAttack4(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == PlayerStateID.Attack3) mFsmSystem.CurrentState.Reason(PlayerStateID.Attack4);
    }

    void CharacterMove(string event_name, object udata)// z is y, x is x
    {
        ROperation replyop = (ROperation)udata;
        print(replyop);
        uint deltatime = replyop.Deltatime;
        int len = replyop.Operations.Count;
        time += deltatime;
        Debug.Log("ToIdleLEN:" + len);
        int flag = 1;
        if (len == 0) flag = -1;
        for (int i = 0; i < len; i++)
        {
            OP op = replyop.Operations[i];
            if (op.Id != id) continue;
            Debug.Log("MOVE::" + op.Move + "  DO::" + op.Do + " DELTATIME::" + deltatime);
            if (op.Move
            && mFsmSystem.CurrentState.StateID != PlayerStateID.Defend
            && mFsmSystem.CurrentState.StateID != PlayerStateID.Attack1
            && mFsmSystem.CurrentState.StateID != PlayerStateID.Attack2
            && mFsmSystem.CurrentState.StateID != PlayerStateID.Attack3
            && mFsmSystem.CurrentState.StateID != PlayerStateID.Attack4)
            {
                EventMgr.Instance.Emit("PlayerMove" + id.ToString(), null);
                Vector3 direc = new Vector3(-ToolMgr.Instance.IntTransFloat(op.Movex, 2), 0, -ToolMgr.Instance.IntTransFloat(op.Movey, 2));
                if (!direc.Equals(Vector3.zero))
                {
                    flag = 0;
                    float deltadis = ((float)speed * deltatime) / 1000;
                    int tmp = ToolMgr.Instance.FloatTransInt(deltadis, 2);
                    deltadis = ToolMgr.Instance.IntTransFloat(tmp, 2);
                    direction = direc;
                    /*ixv += op.Movex;
                    iyv += op.Movey;
                    x = x + ixv * deltatime/1000;
                    z = z + iyv * deltatime/1000;*/
                    if(op.Movex>0)
                    {
                        x -= deltadis;
                    }
                    else if(op.Movex<0)
                    {
                        x += deltadis;
                    }
                    if(op.Movey>0)
                    {
                        z -= deltadis;
                    }
                    else if(op.Movey<0)
                    {
                        z += deltadis;
                    }

                    tmp = ToolMgr.Instance.FloatTransInt(x, 2);
                    x = ToolMgr.Instance.IntTransFloat(tmp, 2);
                    tmp = ToolMgr.Instance.FloatTransInt(z, 2);
                    z = ToolMgr.Instance.IntTransFloat(tmp, 2);
                }
            }
        }
        if (flag==0) EventMgr.Instance.Emit("PlayerMove"+id.ToString(),null);
            else if (flag==1) EventMgr.Instance.Emit("PlayerMoveToIdle"+id.ToString(),null);
    }


    private void Update()
    {
        if(id == IDMgr.Instance.get_id())GetInput();
        mFsmSystem.refresh();
        Debug.Log(mFsmSystem.CurrentState);
    }


    private void FixedUpdate() {
        SendToServer.SendMsgQueue();
    }

    public void MakeFSM()
    {
        mFsmSystem = new PlayerFSM();
        
        PlayerIdleState playerIdleState=new PlayerIdleState(mFsmSystem, this);
        playerIdleState.AddTransition(PlayerTransition.IdleToMove, PlayerStateID.Move);
        playerIdleState.AddTransition(PlayerTransition.IdleToDefend, PlayerStateID.Defend);
        playerIdleState.AddTransition(PlayerTransition.IdleToAttack1, PlayerStateID.Attack1);


        PlayerMoveState playerMoveState = new PlayerMoveState(mFsmSystem, this);
        playerMoveState.AddTransition(PlayerTransition.MoveToIdle, PlayerStateID.Idle);
        playerMoveState.AddTransition(PlayerTransition.MoveToDefend, PlayerStateID.Defend);
        playerMoveState.AddTransition(PlayerTransition.MoveToAttack1, PlayerStateID.Attack1);

        PlayerDefendState playerDefendState = new PlayerDefendState(mFsmSystem, this);
        playerDefendState.AddTransition(PlayerTransition.DefendToIdle, PlayerStateID.Idle);
        playerDefendState.AddTransition(PlayerTransition.DefendToMove, PlayerStateID.Move);
        playerDefendState.AddTransition(PlayerTransition.DefendToAttack1, PlayerStateID.Attack1);

        PlayerAttack1State playerAttack1State = new PlayerAttack1State(mFsmSystem, this);
        playerAttack1State.AddTransition(PlayerTransition.Attack1ToAttack2, PlayerStateID.Attack2);
        playerAttack1State.AddTransition(PlayerTransition.Attack1ToDefend, PlayerStateID.Defend);
        playerAttack1State.AddTransition(PlayerTransition.Attack1ToIdle, PlayerStateID.Idle);
        playerAttack1State.AddTransition(PlayerTransition.Attack1ToMove, PlayerStateID.Move);

        PlayerAttack2State playerAttack2State = new PlayerAttack2State(mFsmSystem, this);
        playerAttack2State.AddTransition(PlayerTransition.Attack2ToAttack3, PlayerStateID.Attack3);
        playerAttack2State.AddTransition(PlayerTransition.Attack2ToDefend, PlayerStateID.Defend);
        playerAttack2State.AddTransition(PlayerTransition.Attack2ToIdle, PlayerStateID.Idle);
        playerAttack2State.AddTransition(PlayerTransition.Attack2ToMove, PlayerStateID.Move);

        PlayerAttack3State playerAttack3State = new PlayerAttack3State(mFsmSystem, this);
        playerAttack3State.AddTransition(PlayerTransition.Attack3ToAttack4, PlayerStateID.Attack4);
        playerAttack3State.AddTransition(PlayerTransition.Attack3ToDefend, PlayerStateID.Defend);
        playerAttack3State.AddTransition(PlayerTransition.Attack3ToIdle, PlayerStateID.Idle);
        playerAttack3State.AddTransition(PlayerTransition.Attack3ToMove, PlayerStateID.Move);

        PlayerAttack4State playerAttack4State = new PlayerAttack4State(mFsmSystem, this);
        playerAttack4State.AddTransition(PlayerTransition.Attack4ToAttack1, PlayerStateID.Attack1);
        playerAttack4State.AddTransition(PlayerTransition.Attack4ToDefend, PlayerStateID.Defend);
        playerAttack4State.AddTransition(PlayerTransition.Attack4ToIdle, PlayerStateID.Idle);
        playerAttack4State.AddTransition(PlayerTransition.Attack4ToMove, PlayerStateID.Move);










        mFsmSystem.AddState(playerIdleState);
        mFsmSystem.AddState(playerMoveState);
        mFsmSystem.AddState(playerDefendState);
        mFsmSystem.AddState(playerAttack1State);
        mFsmSystem.AddState(playerAttack2State);
        mFsmSystem.AddState(playerAttack3State);
        mFsmSystem.AddState(playerAttack4State);
        Debug.Log(1);
    }
    public void Move()
    {
        this.gameObject.transform.position = new Vector3(x, y, z);
        this.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        manimstor.Play("Move");
        //Debug.Log("PlayerMove");
    }


    public void Idle()
    {
        this.gameObject.transform.position = new Vector3(x, y, z);
        this.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        //Debug.Log("PlayerIdle");
        manimstor.Play("Idle");
    }

    public void Defend()
    {
        this.gameObject.transform.position = new Vector3(x, y, z);
        this.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        if (bossSkill1 == 1)
        {
            Debug.Log("is playing music");
            SoundPlayerController.Instance.playEffectId = 1;
            bossSkill1 = 2;
            if (id==1)
            {
                obj1.SetActive(true);
            }
            else
            {
                obj2.SetActive(true);
            }
            StartCoroutine(Disap());
        }
        Debug.Log("PlayerDefend"+id.ToString());
        manimstor.Play("Defend");
    }

    public void Attack1()
    {
        Debug.Log("PlayerAttack1");
        manimstor.Play("Attack1");
    }

    public void Attack2()
    {
        Debug.Log("PlayerAttack2");
        manimstor.Play("Attack2");
    }

    public void Attack3()
    {
        Debug.Log("PlayerAttack3");
        manimstor.Play("Attack3");
    }

    public void Attack4()
    {
        Debug.Log("PlayerAttack4");
        manimstor.Play("Attack4");
    }





    private void GetInput()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            OP op = new OP();
            op.Id = id;
            op.Move = false;
            op.Do = Command.PullK;
            SendToServer.AddOpToPack(op);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            OP op = new OP();
            op.Id = id;
            op.Move = false;
            op.Do = Command.PushK;
            SendToServer.AddOpToPack(op);
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            OP op = new OP();
            op.Id = id;
            op.Move = false;
            op.Do = Command.PullJ;
            SendToServer.AddOpToPack(op);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            OP op = new OP();
            op.Id = id;
            op.Move = false;
            op.Do = Command.PushJ;
            SendToServer.AddOpToPack(op);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            xv += 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            xv += -1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            yv += 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            yv += -1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            xv -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            xv -= -1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            yv -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            yv -= -1;
        }



        float xx = Input.GetAxis("Horizontal");
        int movex = ToolMgr.Instance.FloatTransInt(xx,2);
        float yy = Input.GetAxis("Vertical");
        int movey = ToolMgr.Instance.FloatTransInt(yy,2);
        if (movex==0&&movey==0)
        {
            if (moveflag==1)
            {
                OP op = new OP();
                op.Id = id;
                op.Move = true;
                op.Movex = movex;
                op.Movey = movey;
                op.Do = Command.None;
                SendToServer.AddOpToPack(op);
                moveflag = 0;
            }
        }
        else
        {
            moveflag = 1;
            OP op = new OP();
            op.Id = id;
            op.Move = true;
            op.Movex = movex;
            op.Movey = movey;
            op.Do = Command.None;
            SendToServer.AddOpToPack(op);
        }
        /*if(Input.GetKeyUp(KeyCode.W))W = false;
        if(Input.GetKeyDown(KeyCode.W))W = true;
        if(Input.GetKeyUp(KeyCode.A))A = false;
        if(Input.GetKeyDown(KeyCode.A))A = true;
        if(Input.GetKeyUp(KeyCode.S))S = false;
        if(Input.GetKeyDown(KeyCode.S))S = true;
        if(Input.GetKeyUp(KeyCode.D))D = false;
        if(Input.GetKeyDown(KeyCode.D))D = true;*/
    }




    // Update is called once per frame
    public void takeDamage()
    {
        underAttack = true;
    }
    public void dealDamage()
    {
        if(underAttack)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
            underAttack = false;
            StartCoroutine(BecomeNormal());
        }
    }
    

    IEnumerator BecomeNormal()
    {
        yield return new WaitForSeconds(0.15f);
        obj.GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator Disap()
    {
        yield return new WaitForSeconds(2f);
        if (id == 1)
        {
            obj1.SetActive(false);
        }
        else
        {
            obj2.SetActive(false);
        }
    }

}
