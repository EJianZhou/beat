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

public class RecvFromServer
{
    public static int HEAD_SIZE = 9;
    public static int HEAD_OFFSET = 16;
    private static int offset = 0;
    private static int MAX_BUFFER_SIZE = 1 << 16;
    private static byte[] buffer = new byte[MAX_BUFFER_SIZE];
    public static void Connection(string serverIP, int serverPort) // 从服务器接收数据.
    {

        for (int i = 0; i < MAX_BUFFER_SIZE; i++) buffer[i] = 0x0;
        // 解析服务器IP地址
        IPAddress ipAddress = IPAddress.Parse(serverIP);

        // 连接到服务器
        SocketInfo.clientSocket.Connect(new IPEndPoint(ipAddress, serverPort));

        // 连接成功，开始异步接收数据
        SocketInfo.clientSocket.BeginReceive(buffer, offset, buffer.Length - offset, SocketFlags.None, ReceiveCallback, null);
    }
    private static void ReceiveCallback(IAsyncResult ar)
    {
        Debug.Log(1111111);
        try
        {
            int bytesReceive = SocketInfo.clientSocket.EndReceive(ar);

            if (bytesReceive > 0)
            {
                int bodySize = 0;
                int bodyType = -1;
                int playerid = 0;
                offset += bytesReceive;

                if (offset >= HEAD_SIZE)
                {
                    try
                    {
                        byte[] head = GetResponseHeader(buffer, HEAD_SIZE);
                        bodySize = (int)((head[0] & 0xFF) | ((head[1] & 0xFF) << 8) | ((head[2] & 0xFF) << 16) | ((head[3] & 0xFF) << 24));
                        playerid = (int)((head[4] & 0xFF) | ((head[5] & 0xFF) << 8) | ((head[6] & 0xFF) << 16) | ((head[7] & 0xFF) << 24));
                        bodyType = (int)head[8] & 0xFF;

                    }
                    catch (Exception ex)
                    {
                        offset = 0;
                        bodySize = 0;

                        Debug.LogError("反序列化出错，Exception = " + ex.ToString());

                        // 既然要return， 就要继续开启异步监听，以持续接受服务端的消息
                        SocketInfo.clientSocket.BeginReceive(buffer, offset, MAX_BUFFER_SIZE - offset, SocketFlags.None, ReceiveCallback, null);
                        return;
                    }
                }

                // 处理包体
                while (bodySize > 0 && offset >= bodySize + HEAD_SIZE)
                {
                    // 当前包的包体
                    Debug.Log(bodyType);
                    Debug.Log(bodySize);
                    byte[] body = GetResponseBody(buffer, bodySize);
                    if (bodyType == 2)
                    {
                        // Login Response
                        try
                        {
                            RLogin rLogin = new RLogin();
                            rLogin.MergeFrom(body, 0, bodySize);
                            if (rLogin.Success == 1)
                            {
                                IDMgr.Instance.playerID = rLogin.Id;
                                Debug.Log(rLogin.Id);
                            }
                            EventMgr.Instance.Emit("RLogin", rLogin);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("Error in Receive LoginResponse, Error = " + ex.ToString());
                        }
                    }
                    else if (bodyType == 4)
                    {
                        // Match Response
                        try
                        {
                            RMatch rMatch = new RMatch();
                            rMatch.MergeFrom(body, 0, bodySize);
                            EventMgr.Instance.Emit("RMatch", rMatch);



                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("Error in Receive LoginResponse, Error = " + ex.ToString());
                        }
                    }
                    else if (bodyType == 5)
                    {
                        // Enter Response
                        try
                        {
                            REnter rEnter = new REnter();
                            rEnter.MergeFrom(body, 0, bodySize);
                            EventMgr.Instance.Emit("REnter", rEnter);




                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("Error in Receive LoginResponse, Error = " + ex.ToString());
                        }
                    }
                    else if (bodyType == 7)
                    {
                        // Operation Response
                        try
                        {
                            ROperation rOperation = new ROperation();
                            rOperation.MergeFrom(body, 0, bodySize);
                            EventMgr.Instance.Emit("ROperation", rOperation);




                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("Error in Receive LoginResponse, Error = " + ex.ToString());
                        }
                    }
                    else if (bodyType == 9)
                    {
                        // Exitgame Response
                        try
                        {
                            RExitgame rExitgame = new RExitgame();
                            rExitgame.MergeFrom(body, 0, bodySize);
                            EventMgr.Instance.Emit("RExitgame", rExitgame);




                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("Error in Receive LoginResponse, Error = " + ex.ToString());
                        }
                    }








                    // 因为我的body是包体的大小，这里要把（包头+包体）都去掉，所以先加上 HEAD_SIZE
                    bodySize += HEAD_SIZE;

                    for (int i = 0; i < offset - bodySize; i++)
                    {
                        buffer[i] = buffer[i + bodySize];
                    }

                    offset -= bodySize;
                    bodySize = 0;
                    bodyType = -1;
                    playerid = 0;
                    // 因为会粘包，所以要继续判断后面是否有包

                    if (offset >= HEAD_SIZE)
                    {
                        try
                        {
                            byte[] head = GetResponseHeader(buffer, HEAD_SIZE);
                            bodySize = (int)((head[0] & 0xFF) | ((head[1] & 0xFF) << 8) | ((head[2] & 0xFF) << 16) | ((head[3] & 0xFF) << 24));
                            playerid = (int)((head[4] & 0xFF) | ((head[5] & 0xFF) << 8) | ((head[6] & 0xFF) << 16) | ((head[7] & 0xFF) << 24));
                            bodyType = (int)head[8] & 0xFF;

                        }
                        catch (Exception ex)
                        {
                            offset = 0;
                            bodySize = 0;

                            Debug.LogError("反序列化出错，Exception = " + ex.ToString());
                            break;
                        }
                    }
                }
                SocketInfo.clientSocket.BeginReceive(buffer, offset, buffer.Length - offset, SocketFlags.None, ReceiveCallback, null);
            }

        }
        catch (SocketException ex)
        {
            Console.WriteLine("接收消息出错，错误信息：{0}", ex.Message);
        }
    }

    public static byte[] GetResponseHeader(byte[] buf, int recvLen)
    {
        if (recvLen >= HEAD_SIZE)
        {
            byte[] head = new byte[HEAD_SIZE];

            for (int i = 0; i < HEAD_SIZE; i++)
            {
                head[i] = buf[i];
            }

            return head;
        }

        return null;
    }

    public static byte[] GetResponseBody(byte[] buf, int recvLen)
    {
        if (recvLen > 0)
        {
            byte[] body = new byte[recvLen];

            for (int i = 0; i < recvLen; i++)
            {
                body[i] = buf[i + HEAD_SIZE];
            }

            return body;
        }
        return null;
    }

    public int bytesToInt(byte[] src, int offset)
    {
        int value;
        value = (int)((src[offset + 3] & 0xFF)
                | ((src[offset + 2] & 0xFF) << 8)
                | ((src[offset + 1] & 0xFF) << 16)
                | ((src[offset] & 0xFF) << 24));
        return value;
    }








}








