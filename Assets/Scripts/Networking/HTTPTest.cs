using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;
using Best.HTTP;
using Best.HTTP.Request;
using System;
using System.IO;
using System.Text;
using MyCraft.Models;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public sealed class HTTPTest : MonoBehaviour
{
    [System.Serializable]
    public class RequestData
    {
        public string text;
    }

    [System.Serializable]
    public class ResponseData
    {
        public string chatResponse;
    }

    public GameObject loadingWindow;
    public TMP_Text loadingText; // Use TMP_Text instead of Text
    private string apiUrl = "https://craft-server-api-4a5c605b59f7.herokuapp.com/process";

    private IEnumerator Start()
    {
        ShowLoadingWindow("Starting request...");
        TextLog.Instance.Log("IEnumerator started!");

        var request = HTTPRequest.CreatePost(apiUrl);

        var requestData = new RequestData
        {
            text = "https://littlesunnykitchen.com/marry-me-chicken/"
        };

        string jsonString = JsonConvert.SerializeObject(requestData); // Use JsonConvert.SerializeObject
        byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);
        var uploadStream = new MemoryStream(jsonData);

        request.UploadSettings.UploadStream = uploadStream;
        request.SetHeader("Content-Type", "application/json");

        request.Send();
        yield return request;

        switch (request.State)
        {
            case HTTPRequestStates.Finished:
                if (request.Response.IsSuccess)
                {
                    TextLog.Instance.Log("Upload finished successfully!");
                    string responseText = request.Response.DataAsText;

                    TextLog.Instance.Log("Raw response: " + responseText);

                    ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(responseText); // Use JsonConvert.DeserializeObject
                    TextLog.Instance.Log($"Response: {responseData.chatResponse}");

                    ProcessCraftData(responseData.chatResponse);
                }
                else
                {
                    TextLog.Instance.Log($"Server sent an error: {request.Response.StatusCode} - {request.Response.Message}");
                    TextLog.Instance.Log($"Response Data: {request.Response.DataAsText}");
                    HideLoadingWindow();
                }
                break;

            default:
                TextLog.Instance.Log($"Request finished with error! Request state: {request.State}");
                HideLoadingWindow();
                break;
        }
    }

    private void ProcessCraftData(string jsonData)
    {
        ShowLoadingWindow("Processing craft data...");
        string cleanJsonData = RemoveCommentsFromJson(jsonData);

        Debug.Log("Cleaned JSON Data: " + cleanJsonData);
        try
        {
            // Directly deserialize the cleaned JSON into Craft using JsonConvert
            Craft craft = JsonConvert.DeserializeObject<Craft>(cleanJsonData);

            if (craft != null)
            {
                Debug.Log("Deserialized Craft: " + JsonConvert.SerializeObject(craft, Formatting.Indented));

                if (CraftDataPersist.Instance != null)
                {
                    CraftDataPersist.Instance.ProcessWebSocketData(craft);
                }
                else
                {
                    Debug.LogError("CraftDataPersist.Instance is not initialized");
                }

                HideLoadingWindow();
                SceneManager.LoadScene("CraftScene"); // Replace with your scene name
            }
            else
            {
                Debug.LogError("Failed to parse Craft from cleaned JSON data");
                HideLoadingWindow();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing craft data: " + ex.Message);
            HideLoadingWindow();
        }
    }

    private string RemoveCommentsFromJson(string jsonData)
    {
        // Implement your logic to remove comments from JSON data
        return jsonData;
    }

    private void ShowLoadingWindow(string message)
    {
        if (loadingWindow != null)
        {
            loadingWindow.SetActive(true);
            loadingText.text = message;
        }
    }

    private void HideLoadingWindow()
    {
        if (loadingWindow != null)
        {
            loadingWindow.SetActive(false);
        }
    }
}


// using System;
// using System.Collections;
// using UnityEngine;
// using Best.HTTP;
// using Best.HTTP.Request;
// using System.IO;
// using System.Text;
// using MyCraft.Models;
// using Newtonsoft.Json;

// public sealed class HTTPTest : MonoBehaviour
// {
//     [System.Serializable]
//     public class RequestData
//     {
//         public string text;
//     }

//     [System.Serializable]
//     public class ResponseData
//     {
//         public string chatResponse;
//     }

//     private string apiUrl = "https://craft-server-api-4a5c605b59f7.herokuapp.com/process";

//     private IEnumerator Start()
//     {
//         TextLog.Instance.Log("IEnumerator started!");

//         var request = HTTPRequest.CreatePost(apiUrl);

//         var requestData = new RequestData
//         {
//             text = "https://littlesunnykitchen.com/marry-me-chicken/"
//         };

//         string jsonString = JsonConvert.SerializeObject(requestData); // Use JsonConvert.SerializeObject
//         byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);
//         var uploadStream = new MemoryStream(jsonData);

//         request.UploadSettings.UploadStream = uploadStream;
//         request.SetHeader("Content-Type", "application/json");

//         request.Send();
//         yield return request;

//         switch (request.State)
//         {
//             case HTTPRequestStates.Finished:
//                 if (request.Response.IsSuccess)
//                 {
//                     TextLog.Instance.Log("Upload finished successfully!");
//                     string responseText = request.Response.DataAsText;

//                     TextLog.Instance.Log("Raw response: " + responseText);

//                     ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(responseText); // Use JsonConvert.DeserializeObject
//                     TextLog.Instance.Log($"Response: {responseData.chatResponse}");

//                     ProcessCraftData(responseData.chatResponse);
//                 }
//                 else
//                 {
//                     TextLog.Instance.Log($"Server sent an error: {request.Response.StatusCode} - {request.Response.Message}");
//                     TextLog.Instance.Log($"Response Data: {request.Response.DataAsText}");
//                 }
//                 break;

//             default:
//                 TextLog.Instance.Log($"Request finished with error! Request state: {request.State}");
//                 break;
//         }
//     }

//     private void ProcessCraftData(string jsonData)
//     {
//         string cleanJsonData = RemoveCommentsFromJson(jsonData);

//         Debug.Log("Cleaned JSON Data: " + cleanJsonData);
//         try
//         {
//             // Directly deserialize the cleaned JSON into Craft using JsonConvert
//             Craft craft = JsonConvert.DeserializeObject<Craft>(cleanJsonData);

//             if (craft != null)
//             {
//                 Debug.Log("Deserialized Craft: " + JsonConvert.SerializeObject(craft, Formatting.Indented));

//                 if (CraftDataPersist.Instance != null)
//                 {
//                     CraftDataPersist.Instance.ProcessWebSocketData(craft);
//                 }
//                 else
//                 {
//                     Debug.LogError("CraftDataPersist.Instance is not initialized");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("Failed to parse Craft from cleaned JSON data");
//             }
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError("Error processing craft data: " + ex.Message);
//         }
//     }

//     private string RemoveCommentsFromJson(string jsonData)
//     {
//         // Implement your logic to remove comments from JSON data
//         return jsonData;
//     }
// }

