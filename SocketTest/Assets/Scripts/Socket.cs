using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.IO;
#endif

public class Socket : MonoBehaviour
{
    public TextMesh tm = null;
    static String temp = "OK";
    static bool bDataOK = false;
#if !UNITY_EDITOR
    StreamSocket socket;
    StreamSocketListener listener;
    String port;

#endif

    // Use this for initialization
    void Start()
    {
#if !UNITY_EDITOR
        listener = new StreamSocketListener();
        port = "12345";
        listener.ConnectionReceived += Listener_ConnectionReceived;
        //listener.Control.KeepAlive = false;

        Listener_Start();
#endif
    }

#if !UNITY_EDITOR
    private async void Listener_Start()
    {
        tm.text = "Started";
        Debug.Log("Listener started");
        try
        {
            await listener.BindServiceNameAsync(port);
        }
        catch (Exception e)
        {
            tm.text = "Error: " + e.Message;
            Debug.Log("Error: " + e.Message);
        }
        //tm.text = "Listening~";
        Debug.Log("Listening");
    }

    private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    {
        DataReader reader = new DataReader(args.Socket.InputStream);
        DataWriter writer = new DataWriter(args.Socket.OutputStream);
        try
        {
            while (true)
            {
                //发送数据格式： 命令(1) 长度(1) 数据(长度)
                byte[] ba = { 0 };
                //读取命令，1字节
                uint actualLength = await reader.LoadAsync(1);
                if (actualLength != 1)
                    return;//socket提前close()了                               
                uint command = uint.Parse(reader.ReadByte().ToString()); //解析命令

                //读取数据长度, 1字节
                actualLength = await reader.LoadAsync(1);
                if (actualLength != 1)
                    return;
                uint dataLength = uint.Parse(reader.ReadByte().ToString()); ; //解析数据长度

                Matrix4x4[] matrixGroup;

                //读取所有数据，matrixlength长度
                if (dataLength != 0)
                {
                    actualLength = await reader.LoadAsync(dataLength);
                    if (actualLength != dataLength)
                    {
                        return;//socket提前close()了
                    }

                    uint numberOfMatrix = dataLength / 64;

                    if (numberOfMatrix < 1)
                    {
                        return; //未收到一个矩阵
                    }

                    matrixGroup = new Matrix4x4[numberOfMatrix];

                    for (int i = 0; i < numberOfMatrix; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            float[] column = new float[4];
                            for (int k = 0; k < 4; k++)
                            {
                                byte[] temp = new byte[4];
                                reader.ReadBytes(temp);
                                column[k] = BitConverter.ToSingle(temp, 0);
                            }
                            matrixGroup[i].SetColumn(j, new Vector4(column[0], column[1], column[2], column[3]));
                        }
                    }
                }


                //返回一个初始化的矩阵做测试
                Matrix4x4 mat = new Matrix4x4(); //这里为要发送的矩阵
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        byte[] b = BitConverter.GetBytes(mat[j, i]);
                        writer.WriteBytes(b);
                    }
                }
                await writer.StoreAsync();
            }
        }
        catch (Exception exception)
        {
            // If this is an unknown status it means that the error if fatal and retry will likely fail.
            if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
            {
                throw;
            }
        }
    }
#endif
    // Update is called once per frame
    void Update()
    {
        if(bDataOK == true)
        {
            tm.text = temp;
            bDataOK = false;
        }
      //  tm.text = temp;
    }
}
