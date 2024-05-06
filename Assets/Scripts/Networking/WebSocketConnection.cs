// using System;
// using System.Text;
// using System.Threading.Tasks;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using WebSocketSharp;
// using System.Linq;


// public class WebSocketExample : MonoBehaviour
// {
//     private TextLog textLog;
//     private WebSocket ws;
//     private Queue<string> messageQueue = new Queue<string>();

//     private void Start()
//     {
//         // ws = new WebSocket("ws://craftcast-ws-365cc828bed7.herokuapp.com/");
//         ws = new WebSocket("ws://localhost:3000/");
//         // ws.binaryType = "arrayBuffer";

//         ws.OnOpen += OnOpen;
//         ws.OnMessage += OnMessage;
//         ws.OnClose += OnClose;
//         ws.Connect();

//         StartCoroutine(SendHeartbeat());
//     }

//     private void OnDestroy()
//     {
//         if (ws != null)
//         {
//             ws.Close();
//             ws = null;
//         }
//     }

//     private void OnOpen(object sender, EventArgs e)
//     {
//         TextLog.Instance.Log("Connected to server");
//         MainThreadDispatcher.Enqueue(() => TextLog.Instance.Log("Connected to server"));
//     }

//     private void OnMessage(object sender, MessageEventArgs e)
//     {
//         TextLog.Instance.Log("Message event received"); // Ensure this gets logged first
//         MainThreadDispatcher.Enqueue(() =>
//         {
//             if (e.IsText)
//             {
//                 TextLog.Instance.Log("Received text: " + e.Data);
//             }
//             else if (e.IsBinary)
//             {
//                 string receivedText = Encoding.UTF8.GetString(e.RawData);
//                 TextLog.Instance.Log("Received binary data of length " + e.RawData.Length);
//                 TextLog.Instance.Log("Converted binary to text: " + receivedText);
//             }
//         });
//     }



//     // private void OnMessage(object sender, MessageEventArgs e)
//     // {
//     //     TextLog.Instance.Log("Message event received" + e.RawData.Length);
//     //     // HandleBinaryData(e.RawData);
//     //     string receivedText = Encoding.UTF8.GetString(e.RawData);
//     //     TextLog.Instance.Log("Sending binary data to be processed " + receivedText);

//     //     // lock (messageQueue)
//     //     // {
//     //     //     messageQueue.Enqueue(e.Data);
//     //     // }
//     //     // TextLog.Instance.Log("Message event received");
//     //     // if (string.IsNullOrEmpty(e.Data))
//     //     // {
//     //     //     TextLog.Instance.Log("Received empty message");
//     //     // }
//     //     // else
//     //     // {
//     //     //     TextLog.Instance.Log("Received message: " + e.Data);
//     //     //     lock (messageQueue)
//     //     //     {
//     //     //         messageQueue.Enqueue(e.Data);
//     //     //     }
//     //     // }

//     // }

//     private void OnClose(object sender, CloseEventArgs e)
//     {
//         TextLog.Instance.Log("Connection closed");
//     }

//     private void ProcessMessages()
//     {
//         if (messageQueue.Count > 0)
//         {
//             lock (messageQueue)
//             {
//                 while (messageQueue.Count > 0)
//                 {
//                     string message = messageQueue.Dequeue();

//                     // Log the JSON string; you could also parse and process the JSON further if needed
//                     TextLog.Instance.Log("Processed on main thread: " + message);
//                     ProcessData(message);
//                 }
//             }
//         }
//     }

//     private void ProcessData(string data)
//     {
//         try
//         {
//             CraftData craftData = JsonUtility.FromJson<CraftData>(data);
//             if (craftData != null && craftData.Craft != null)
//             {
//                 TextLog.Instance.Log("Processing craft with ID: " + craftData.Craft.Craft_ID);
//                 CraftDataPersist.Instance.ProcessWebSocketData(data);
//                 if (craftData.Steps != null)
//                 {
//                     TextLog.Instance.Log("Loading steps for craft: " + craftData.Craft.Craft_ID);
//                     StepDataPersist.Instance.LoadStepsForCraft(craftData.Craft.Craft_ID, craftData.Steps);
//                 }
//                 if (craftData.Items != null)
//                 {
//                     TextLog.Instance.Log("Loading items for craft: " + craftData.Craft.Craft_ID);
//                     ItemDataPersist.Instance.LoadItemsForCraft(craftData.Craft.Craft_ID, craftData.Items);
//                 }
//             }
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Exception during WebSocket message handling: " + e.ToString());
//         }
//     }

//     // Keep the connection alive
//     IEnumerator SendHeartbeat()
//     {
//         while (ws.IsAlive)
//         {
//             if (ws.ReadyState == WebSocketState.Open)
//             {
//                 ws.Send("ping");
//             }
//             yield return new WaitForSeconds(30); // Send ping every 30 seconds
//         }
//     }

//     void HandleBinaryData(byte[] rawData)
//     {
//         try
//         {
//             if (rawData.Length > 0)
//             {
//                 switch (rawData[0])  // Assuming the first byte indicates data type
//                 {
//                     case 0x01:  // Example case for text data
//                         string text = Encoding.UTF8.GetString(rawData.Skip(1).ToArray());
//                         TextLog.Instance.Log("Processed text data: " + text);
//                         break;
//                     default:
//                         TextLog.Instance.Log("Unhandled data type");
//                         break;
//                 }
//             }
//             else
//             {
//                 TextLog.Instance.Log("Received empty data");
//             }
//         }
//         catch (Exception ex)
//         {
//             TextLog.Instance.Log("Error processing binary data: " + ex.Message);
//         }
//     }



// }


// // using System;
// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;
// // using WebSocketSharp;
// // using UnityEngine.SceneManagement;

// // public class WebSocketExample : MonoBehaviour
// // {
// //     private TextLog textLog;
// //     private WebSocket ws;
// //     private bool dataReceived = false;
// //     public DirectionManager directionManager;
// //     private Queue<string> messageQueue = new Queue<string>();


// //     void Start()
// //     {
// //         ws = new WebSocket("ws://craftcast-ws-365cc828bed7.herokuapp.com/");

// //         ws.OnOpen += (sender, e) =>
// //         {
// //             TextLog.Instance.Log("Connected to server");
// //         };

// //         ws.OnMessage += (sender, e) =>
// //         {
// //             // Enqueue the message to be processed on the main thread
// //             lock (messageQueue)
// //             {
// //                 messageQueue.Enqueue(e.Data);
// //             }
// //         };

// //         ws.OnClose += (sender, e) =>
// //         {
// //             TextLog.Instance.Log("Connection closed");
// //         };

// //         ws.Connect();
// //     }

// //     void Update()
// //     {
// //         // Process messages on the main thread
// //         if (messageQueue.Count > 0)
// //         {
// //             lock (messageQueue)
// //             {
// //                 while (messageQueue.Count > 0)
// //                 {
// //                     var message = messageQueue.Dequeue();
// //                     TextLog.Instance.Log("Processed on main thread: " + message);
// //                     // Here you can safely interact with Unity's API
// //                 }
// //             }
// //         }
// //     }

// //     void OnDestroy()
// //     {
// //         if (ws != null)
// //         {
// //             ws.Close();
// //             ws = null;
// //         }
// //     }

// //     private void OnMessage(object sender, MessageEventArgs e)
// //     {
// //         if (e.IsText)
// //         {
// //             ProcessData(e.Data);  // e.Data is a string containing the message text
// //         }
// //         else if (e.IsBinary)
// //         {
// //             ProcessData(e.RawData);  // e.RawData is a byte[] containing the binary message
// //         }
// //     }



// //     void ProcessData(byte[] bytes)
// //     {
// //         try
// //         {
// //             string json = System.Text.Encoding.UTF8.GetString(bytes);
// //             TextLog.Instance.Log("Received JSON data: " + json);

// //             CraftData craftData = JsonUtility.FromJson<CraftData>(json);
// //             if (craftData != null && craftData.Craft != null)
// //             {
// //                 TextLog.Instance.Log("Processing craft with ID: " + craftData.Craft.Craft_ID);
// //                 CraftDataPersist.Instance.ProcessWebSocketData(json);
// //                 if (craftData.Steps != null)
// //                 {
// //                     TextLog.Instance.Log("Loading steps for craft: " + craftData.Craft.Craft_ID);
// //                     StepDataPersist.Instance.LoadStepsForCraft(craftData.Craft.Craft_ID, craftData.Steps);
// //                 }
// //                 if (craftData.Items != null)
// //                 {
// //                     TextLog.Instance.Log("Loading items for craft: " + craftData.Craft.Craft_ID);
// //                     ItemDataPersist.Instance.LoadItemsForCraft(craftData.Craft.Craft_ID, craftData.Items);
// //                 }
// //             }
// //         }
// //         catch (Exception e)
// //         {
// //             Debug.LogError("Exception during WebSocket message handling: " + e.ToString());
// //         }
// //     }

// //     IEnumerator LoadCraftSceneAsync()
// //     {
// //         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CraftScene");

// //         // Wait until the asynchronous scene fully loads
// //         while (!asyncLoad.isDone)
// //         {
// //             // Here you can add a loading screen or update a progress bar
// //             yield return null;
// //         }
// //         // Post-load operations, if any
// //     }
// // }
