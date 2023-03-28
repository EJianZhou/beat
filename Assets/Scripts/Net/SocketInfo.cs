using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using System;

public class SocketInfo : MonoBehaviour
{
    private static SocketInfo _instance;
    public static SocketInfo Instance
    {
        get
        {
            return _instance;
        }
    }

    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private void Awake()
    {
        /*
        IPAddress ipAddress = IPAddress.Parse("10.0.128.153");
        IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, 8080);
        clientSocket.Connect(serverEndPoint);
        */
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        RecvFromServer.Connection("10.0.150.37", NetConfig.SERVER_PORT);
        
    }
    public static void CloseSocket()
    {
        // 关闭socket连接.
        SocketInfo.clientSocket.Shutdown(SocketShutdown.Both);
        SocketInfo.clientSocket.Close();
    }

    public void OnDestroy()
    {
        CloseSocket();
    }
}
