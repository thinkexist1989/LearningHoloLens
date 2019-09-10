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
            while(true)
            {
                uint x = await reader.LoadAsync(5);

                String a = reader.ReadString(x);
                writer.WriteString(a);
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
