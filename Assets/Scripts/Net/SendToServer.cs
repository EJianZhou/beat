using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using Beatsgame;
using System;

public class SendToServer
{
    public static int bufferlength = 0;
    public static byte[] buffer = new byte[1 << 16];

    static Operation opMsg = new Operation();
    public static void AddMsgToQueue(Login login)
    {
        Byte[] sendMsg = new Byte[1234];
        Byte[] msgLen = new Byte[4];
        Byte[] msgId = new byte[4];
        Byte[] msgType = new Byte[1];
        int len = login.CalculateSize();
        msgLen = intToBytes(len, 4);
        msgId = intToBytes(IDMgr.Instance.playerID, 4);
        msgType = intToBytes(NetConfig.LOGIN, 1);
        msgLen.CopyTo(sendMsg, 0);
        msgId.CopyTo(sendMsg, 4);
        msgType.CopyTo(sendMsg, 8);
        login.ToByteArray().CopyTo(sendMsg, RecvFromServer.HEAD_SIZE);
        sendMsg.CopyTo(buffer, bufferlength);
        bufferlength += RecvFromServer.HEAD_SIZE + len;
    }

    public static void AddMsgToQueue(Match match)
    {
        Byte[] sendMsg = new Byte[1234];
        Byte[] msgLen = new Byte[4];
        Byte[] msgId = new byte[4];
        Byte[] msgType = new Byte[1];
        int len = match.CalculateSize();
        msgLen = intToBytes(len, 4);
        msgId = intToBytes(IDMgr.Instance.playerID, 4);
        for (int i=0;i<4;i++)
        {
            Debug.Log(msgId[i]);
        }
        Debug.Log("ymatch1" + IDMgr.Instance.playerID);
        Debug.Log("ymatch2" + (int)IDMgr.Instance.playerID);
        msgType = intToBytes(NetConfig.MATCH, 1);
        msgLen.CopyTo(sendMsg, 0);
        msgId.CopyTo(sendMsg, 4);
        msgType.CopyTo(sendMsg, 8);
        match.ToByteArray().CopyTo(sendMsg, RecvFromServer.HEAD_SIZE);
        //SocketInfo.clientSocket.Send(sendMsg, len+4, 0);
        sendMsg.CopyTo(buffer, bufferlength);
        bufferlength += RecvFromServer.HEAD_SIZE + len;
    }

    public static void AddMsgToQueue(Operation operation)
    {
        Byte[] sendMsg = new Byte[1234];
        Byte[] msgLen = new Byte[4];
        Byte[] msgId = new byte[4];
        Byte[] msgType = new Byte[1];
        int len = operation.CalculateSize();
        msgLen = intToBytes(len, 4);
        msgId = intToBytes(IDMgr.Instance.playerID, 4);
        msgType = intToBytes(NetConfig.OPERATION, 1);
        msgLen.CopyTo(sendMsg, 0);
        msgId.CopyTo(sendMsg, 4);
        msgType.CopyTo(sendMsg, 8);
        operation.ToByteArray().CopyTo(sendMsg, RecvFromServer.HEAD_SIZE);
        //SocketInfo.clientSocket.Send(sendMsg, len+4, 0);

        sendMsg.CopyTo(buffer, bufferlength);
        bufferlength += RecvFromServer.HEAD_SIZE + len;
    }

    public static void AddMsgToQueue(Exitgame exitgame)
    {
        Byte[] sendMsg = new Byte[1234];
        Byte[] msgLen = new Byte[4];
        Byte[] msgId = new byte[4];
        Byte[] msgType = new Byte[1];
        int len = exitgame.CalculateSize();
        msgLen = intToBytes(len, 4);
        msgId = intToBytes(IDMgr.Instance.playerID, 4);
        msgType = intToBytes(1, 1);
        msgLen.CopyTo(sendMsg, 0);
        msgId.CopyTo(sendMsg, 4);
        msgType.CopyTo(sendMsg, 8);
        exitgame.ToByteArray().CopyTo(sendMsg, RecvFromServer.HEAD_SIZE);
        //SocketInfo.clientSocket.Send(sendMsg, len+4, 0);
        sendMsg.CopyTo(buffer, bufferlength);
        bufferlength += RecvFromServer.HEAD_SIZE + len;
    }

    public static void SendMsgQueue()
    {
        Debug.Log("COUNT:" + opMsg.ClientOperation.Count);
        if (opMsg.ClientOperation.Count > 0)
        {
            AddMsgToQueue(opMsg);
            Debug.Log(opMsg + " " + bufferlength);
            opMsg.ClientOperation.Clear();

        }

        SocketInfo.clientSocket.Send(buffer, bufferlength, 0);
        bufferlength = 0;
        Array.Clear(buffer, 0, buffer.Length);
    }

    public static void AddOpToPack(OP operation)
    {
        opMsg.ClientOperation.Add(operation);
    }


    public static byte[] intToBytes(int value, int length)
    {
        byte[] src = new byte[length];
        for (int i = 0; i < length; i++)
        {
            src[i] = (byte)((value >> (i * 8)) & 0xFF);
        }
        return src;
    }

    public static byte[] uintToBytes(uint value, int length)
    {
        byte[] src = new byte[length];
        for (int i = 0; i < length; i++)
        {
            src[i] = (byte)((value >> (i * 8)) & 0xFF);
        }
        return src;
    }

}