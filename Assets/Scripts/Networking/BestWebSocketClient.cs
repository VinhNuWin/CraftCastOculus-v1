using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Best.WebSockets;

public class BestWebSocketClient : MonoBehaviour
{
    private WebSocket webSocket;
    private Queue<string> messageQueue = new Queue<string>();

    void Start()
    {
        // URL to the WebSocket server.
        Uri uri = new Uri("ws://localhost:3000/");

        // Create the WebSocket instance.
        webSocket = new WebSocket(uri);

        // Assign callback for when the connection is established.
        webSocket.OnOpen += OnOpen;

        // Assign callback for when a message is received.
        webSocket.OnMessage += OnMessageReceived;

        // webSocket.OnBinary += OnBinaryMessageReceived;

        // Assign callback for when the connection is closed.
        webSocket.OnClosed += OnWebSocketClosed;

        // Open the connection.
        webSocket.Open();
    }

    private void OnOpen(WebSocket webSocket)
    {
        TextLog.Instance.Log("Connection opened!");
        // Send a message to the server.
        webSocket.Send("Hello Server from unity!");
    }

    private void OnMessageReceived(WebSocket webSocket, string message)
    {
        MainThreadDispatcher.Enqueue(() =>
        {
            TextLog.Instance.Log("Connection opened!" + message);
        });
    }


    // private void OnBinaryMessageReceived(WebSocket webSocket, BufferSegment buffer)
    // {
    //     Debug.Log("Binary Message received from server. Length: " + buffer.Length);

    //     using (var stream = System.IO.File.OpenWrite("path\to\file"))
    //     {
    //         // ✅ Good:
    //         stream.Write(buffer.Data, buffer.Offset, buffer.Count);
    //     }
    // }


    private void OnWebSocketClosed(WebSocket webSocket, WebSocketStatusCodes code, string message)
    {
        TextLog.Instance.Log("WebSocket is now Closed!");

        if (code == WebSocketStatusCodes.NormalClosure)
        {
            // Closed by request
        }
        else
        {
            // Error or forced closure
            TextLog.Instance.Log("WebSocket closed with error: " + message);
        }
    }

    void OnDestroy()
    {
        // Ensure the WebSocket is closed when the GameObject is destroyed
        if (webSocket != null)
        {
            webSocket.Close();
            webSocket = null;
        }
    }
}
