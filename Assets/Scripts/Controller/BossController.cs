using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject obj1,obj2;
    private int attackHash;
    private BossFSM mFsmSystem;
    private Animator manimstor;
    public Beats beats;
    private void Awake()
    {
        Debug.Log(1);
        MakeFSM();
        manimstor = GetComponent<Animator>();
        EventMgr.Instance.AddListener("BossAttack1", ToAttack1);
        EventMgr.Instance.AddListener("BossIdle", ToIdle);
    }
    void ToAttack1(string event_name, object udata)
    {
        Debug.Log("切换到攻击");
        if (mFsmSystem.CurrentState.StateID==BossStateID.Idle) mFsmSystem.CurrentState.Reason(BossStateID.Attack1);
    }

    void ToAttack2(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == BossStateID.Idle) mFsmSystem.CurrentState.Reason(BossStateID.Attack2);
    }

    void ToAttack3(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == BossStateID.Idle) mFsmSystem.CurrentState.Reason(BossStateID.Attack3);
    }

    void ToAttack4(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == BossStateID.Idle) mFsmSystem.CurrentState.Reason(BossStateID.Attack4);
    }

    void ToAttack5(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == BossStateID.Idle) mFsmSystem.CurrentState.Reason(BossStateID.Attack5);
    }

    void ToIdle(string event_name, object udata)
    {
        if (mFsmSystem.CurrentState.StateID == BossStateID.Attack1) mFsmSystem.CurrentState.Reason(BossStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == BossStateID.Attack2) mFsmSystem.CurrentState.Reason(BossStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == BossStateID.Attack3) mFsmSystem.CurrentState.Reason(BossStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == BossStateID.Attack4) mFsmSystem.CurrentState.Reason(BossStateID.Idle);
        if (mFsmSystem.CurrentState.StateID == BossStateID.Attack5) mFsmSystem.CurrentState.Reason(BossStateID.Idle);

    }





    private void Update()
    {
        Debug.Log(mFsmSystem.CurrentState.StateID);
        mFsmSystem.refresh(beats);
    }

    public void MakeFSM()
    {
        mFsmSystem = new BossFSM();

        BossIdleState bossIdleState = new BossIdleState(mFsmSystem, this);
        bossIdleState.AddTransition(BossTransition.IdleToAttack1, BossStateID.Attack1);
        bossIdleState.AddTransition(BossTransition.IdleToAttack2, BossStateID.Attack2);
        bossIdleState.AddTransition(BossTransition.IdleToAttack3, BossStateID.Attack3);
        bossIdleState.AddTransition(BossTransition.IdleToAttack4, BossStateID.Attack4);
        bossIdleState.AddTransition(BossTransition.IdleToAttack5, BossStateID.Attack5);

        BossAttack1State bossAttack1State = new BossAttack1State(mFsmSystem, this);
        bossAttack1State.AddTransition(BossTransition.Attack1ToIdle, BossStateID.Idle);

        BossAttack2State bossAttack2State = new BossAttack2State(mFsmSystem, this);
        bossAttack2State.AddTransition(BossTransition.Attack2ToIdle, BossStateID.Idle);

        BossAttack3State bossAttack3State = new BossAttack3State(mFsmSystem, this);
        bossAttack3State.AddTransition(BossTransition.Attack3ToIdle, BossStateID.Idle);

        BossAttack4State bossAttack4State = new BossAttack4State(mFsmSystem, this);
        bossAttack4State.AddTransition(BossTransition.Attack4ToIdle, BossStateID.Idle);

        BossAttack5State bossAttack5State = new BossAttack5State(mFsmSystem, this);
        bossAttack5State.AddTransition(BossTransition.Attack5ToIdle, BossStateID.Idle);

        mFsmSystem.AddState(bossIdleState);
        mFsmSystem.AddState(bossAttack1State);
        mFsmSystem.AddState(bossAttack2State);
        mFsmSystem.AddState(bossAttack3State);
        mFsmSystem.AddState(bossAttack4State);
        mFsmSystem.AddState(bossAttack5State);





        Debug.Log(1);


    }

    public void Attack1()
    {
        manimstor.Play("Attack1");
        obj1.SetActive(true);
        obj2.SetActive(true);
        Debug.Log("播放Boss攻击动画1....");
    }

    public void Attack2()
    {
        manimstor.Play("Attack2");
        Debug.Log("播放Boss攻击动画2....");
    }

    public void Attack3()
    {
        manimstor.Play("Attack3");
        Debug.Log("播放Boss攻击动画3....");
    }

    public void Attack4()
    {
        manimstor.Play("Attack4");
        Debug.Log("播放Boss攻击动画4....");
    }

    public void Attack5()
    {
        manimstor.Play("Attack5");
        Debug.Log("播放Boss攻击动画5....");
    }



    public void Idle()
    {
        obj1.SetActive(false);
        obj2.SetActive(false);
        manimstor.Play("Idle");
        Debug.Log("播放 Boss Idle 动画中....");
    }

}