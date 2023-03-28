using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Beatsgame;
public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void clicked()
    {
        Login pack = new Login();
        pack.Register = false;
        pack.Username = "admin";
        pack.Password = "admin";
        SendToServer.AddMsgToQueue(pack);
        SendToServer.SendMsgQueue();
        EventMgr.Instance.AddListener("RLogin", onMessageBack);
        EventMgr.Instance.AddListener("REnter", onMessageBack);
    }

    void onMessageBack(string event_name, object data)
    {
        EventMgr.Instance.RemoveListener("RLogin", onMessageBack);
        EventMgr.Instance.RemoveListener("REnter", onMessageBack);
        UIRoot.Instance.uninit("MainMenu", 0);
        if (event_name == "REnter")
        {
            UIRoot.Instance.TurnTo(1);
        }
        else if (event_name == "RLogin")
        {
            UIRoot.Instance.init("Prefabs", "GameLobby", "FunctionLayer", 0);
        }
        else
        {
            Debug.Log($"unknow event : {event_name}");
        }
    }
}