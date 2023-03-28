using Beatsgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMatch : MonoBehaviour
{
    private Button matchBtn;
    public Text text;
    void Awake()
    {
        matchBtn = GetComponent<Button>();
        matchBtn.onClick.AddListener(MatchClick);
    }
    public void MatchClick()
    {
        Match match = new Match();
        match.Id = IDMgr.Instance.playerID;
        SendToServer.AddMsgToQueue(match);
        SendToServer.SendMsgQueue();
        EventMgr.Instance.AddListener("RMatch", onRMatchMessageBack);
    }
    void onRMatchMessageBack(string event_name, object data)
    {
        EventMgr.Instance.RemoveListener("RMatch", onRMatchMessageBack);
        EventMgr.Instance.AddListener("REnter", onREnterMessageBack);

    }
    void onREnterMessageBack(string event_name, object data)
    {
        EventMgr.Instance.RemoveListener("REnter", onRMatchMessageBack);
        UIRoot.Instance.uninit("GameLobby", 0);
        UIRoot.Instance.TurnTo(1);
    }
}