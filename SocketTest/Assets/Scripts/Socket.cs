using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
#endif

public class Socket : MonoBehaviour
{
    public TextMesh tm = null;
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
        port = "8888";
        listener.ConnectionReceived += Listener_ConnectionReceived;
        listener.Control.KeepAlive = false;

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
            Debug.Log("Error: " + e.Message);
        }
        tm.text = "Listening";
        Debug.Log("Listening");
    }

    private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    {
        tm.text = "Connected";
        Debug.Log("Connection received");
        DataReader reader = new DataReader(args.Socket.InputStream);
        try
        {
            while (true)
            {
                // Read first 4 bytes (length of the subsequent string). 
                uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));
                if (sizeFieldCount != sizeof(uint))
                {
                    // The underlying socket was closed before we were able to read the whole data. 
                    return;
                }

                // Read the string. 
                uint stringLength = reader.ReadUInt32();
                uint actualStringLength = await reader.LoadAsync(stringLength);
                if (stringLength != actualStringLength)
                {
                    // The underlying socket was closed before we were able to read the whole data.
                    return;
                }

                // dump data
                tm.text = "hello";
                Debug.Log("Received: " + reader.ReadString(actualStringLength));
            }
        }
        catch (Exception exception)
        {
            // If this is an unknown status it means that the error is fatal and retry will likely fail. 
            if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
            {
                throw;
            }

            // dump data
            tm.text = "Byebye";
            Debug.Log("Read Stream failed: " + exception.Message);
        }
    }
#endif
    // Update is called once per frame
    void Update()
    {
        //tm.text = "hel";
    }
}
