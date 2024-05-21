using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using Best.WebSockets;
using Best.HTTP.Shared.PlatformSupport.Memory;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using MyCraft.Models;

public class BestWebSocketClient : MonoBehaviour
{
    private WebSocket webSocket;
    private Queue<string> messageQueue = new Queue<string>();
    private string rawJsonData;


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
    }

    private void OnMessageReceived(WebSocket webSocket, string message)
    {
        MainThreadDispatcher.Enqueue(() =>
        {
            try
            {
                // Attempt to parse the JSON string to extract the binary data
                var jsonObject = JsonUtility.FromJson<Buffer>(message);
                if (jsonObject.type == "Buffer" && jsonObject.data != null)
                {
                    // Convert the integer array back to byte array
                    byte[] bytes = new byte[jsonObject.data.Length];
                    for (int i = 0; i < jsonObject.data.Length; i++)
                    {
                        bytes[i] = (byte)jsonObject.data[i];
                    }

                    // Now you have the original byte array and can handle it accordingly
                    string jsonData = Encoding.UTF8.GetString(bytes);
                    // TextLog.Instance.Log("Processed Binary Data: " + jsonData);
                    Debug.Log("Processed Binary Data: " + jsonData);
                    ProcessCraftData(jsonData);
                }
            }
            catch (Exception ex)
            {
                TextLog.Instance.Log("Failed to process message: " + ex.Message);
            }
        });
    }
    private string RemoveCommentsFromJson(string jsonData)
    {
        // Regex to find patterns that start with // and end with milliseconds
        string pattern = @"//.*?milliseconds";
        // Replace matched patterns with an empty string
        string cleanedJson = Regex.Replace(jsonData, pattern, string.Empty, RegexOptions.Singleline);
        return cleanedJson;
    }

    private void ProcessCraftData(string jsonData)
    {
        // First remove comments from the JSON string
        string cleanJsonData = RemoveCommentsFromJson(jsonData);

        Debug.Log("Cleaned JSON Data: " + cleanJsonData);
        try
        {
            ChatResponse response = JsonUtility.FromJson<ChatResponse>(cleanJsonData);
            if (response != null && !string.IsNullOrEmpty(response.chatResponse))
            {
                Debug.Log("Inner JSON Data: " + response.chatResponse);
                Craft craft = JsonUtility.FromJson<Craft>(response.chatResponse);

                if (craft != null)
                {
                    if (CraftDataPersist.Instance != null)
                    {
                        CraftDataPersist.Instance.ProcessWebSocketData(craft);
                    }
                    else
                    {
                        Debug.LogError("CraftDataPersist.Instance is not initialized");
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse Craft from chatResponse");
                }
            }
            else
            {
                Debug.LogError("Failed to deserialize ChatResponse or chatResponse is empty");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing craft data: " + ex.Message);
        }
    }

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

[Serializable]
public class Buffer
{
    public string type;
    public int[] data;
}

public class ChatResponse
{
    public string chatResponse;
}