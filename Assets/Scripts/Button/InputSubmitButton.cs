// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using Best.HTTP;
// using Best.HTTP.Request.Upload.Forms;

// public class InputSubmitButton : MonoBehaviour
// {
//     [SerializeField]
//     public Button sendButton;
//     [SerializeField]
//     private Text displayText;  // Ensure this is assigned in the inspector
//     public string apiUrl = "https://craft-server-api-4a5c605b59f7.herokuapp.com/process";

//     private string inputText;
//     private bool isButtonClicked = false;

//     private void Start()
//     {
//         sendButton.onClick.RemoveAllListeners();
//         sendButton.onClick.AddListener(OnSendButtonClick);
//         TextLog.Instance.Log("Button click listener added.");
//         TextLog.Instance.Log("API URL set to: " + apiUrl);

//         // Debug log for event listener count
//         TextLog.Instance.Log("Button onClick listener count: " + sendButton.onClick.GetPersistentEventCount());
//     }

//     private void Update()
//     {
//         // Update inputText from displayText in the Update method
//         inputText = displayText.text;
//     }

//     private void OnSendButtonClick()
//     {
//         isButtonClicked = true; // Set the flag when the button is clicked
//         SubmitInput();
//     }

//     public void SubmitInput()
//     {
//         if (!isButtonClicked)
//         {
//             // TextLog.Instance.Log("SubmitInput called without button click, ignoring.");
//             return; // Ignore calls unless the button was clicked
//         }

//         TextLog.Instance.Log("SubmitInput called");
//         StartCoroutine(SendDataToAPI(inputText));
//         TextLog.Instance.Log("Input Submitted: " + inputText);

//         isButtonClicked = false; // Reset the flag
//     }

//     public void SendDataToAPI()
// }

using UnityEngine;
using Best.HTTP;
using Best.HTTP.Request;
using Best.HTTP.Request.Upload.Forms;
using System.Collections;
using System.IO;
using System.Text;

public sealed class InputSubmitButton : MonoBehaviour
{
    private IEnumerator Start()
    {
        TextLog.Instance.Log("IEnumerator started!");
        // 1. Create request
        var request = HTTPRequest.CreatePost("https://craft-server-api-4a5c605b59f7.herokuapp.com/process");

        // 2. Setup JSON data stream
        var jsonPayload = new
        {
            url = "https://littlesunnykitchen.com/marry-me-chicken/"
        };
        string jsonString = JsonUtility.ToJson(jsonPayload);
        byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);
        var uploadStream = new MemoryStream(jsonData);

        request.UploadSettings.UploadStream = uploadStream;

        // 3. Add headers
        request.SetHeader("Content-Type", "application/json");

        // 4. Send request
        request.Send();

        // 5. Wait for completion
        yield return request;

        switch (request.State)
        {
            case HTTPRequestStates.Finished:
                // 6. Process response
                if (request.Response.IsSuccess)
                {
                    TextLog.Instance.Log("Upload finished successfully!");
                }
                else
                {
                    // 7. Error handling
                    TextLog.Instance.Log($"Server sent an error: {request.Response.StatusCode} - {request.Response.Message}");
                    TextLog.Instance.Log($"Response Data: {request.Response.DataAsText}");
                }
                break;

            // 7. Error handling
            default:
                TextLog.Instance.Log($"Request finished with error! Request state: {request.State}");
                break;
        }
    }
}



// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using Best.HTTP;
// using Best.HTTP.Request.Upload.Forms;

// public class InputSubmitButton : MonoBehaviour
// {
//     [SerializeField]
//     public Button sendButton;
//     [SerializeField]
//     private Text displayText;  // Ensure this is assigned in the inspector
//     public string apiUrl = "https://craft-server-api-4a5c605b59f7.herokuapp.com/process";

//     private string inputText;
//     private bool isButtonClicked = false;

//     private void Start()
//     {
//         sendButton.onClick.RemoveAllListeners();
//         sendButton.onClick.AddListener(OnSendButtonClick);
//         TextLog.Instance.Log("Button click listener added.");
//         TextLog.Instance.Log("API URL set to: " + apiUrl);

//         // Debug log for event listener count
//         TextLog.Instance.Log("Button onClick listener count: " + sendButton.onClick.GetPersistentEventCount());
//     }

//     private void Update()
//     {
//         // Update inputText from displayText in the Update method
//         inputText = displayText.text;
//     }

//     private void OnSendButtonClick()
//     {
//         isButtonClicked = true; // Set the flag when the button is clicked
//         SubmitInput();
//     }

//     public void SubmitInput()
//     {
//         if (!isButtonClicked)
//         {
//             // TextLog.Instance.Log("SubmitInput called without button click, ignoring.");
//             return; // Ignore calls unless the button was clicked
//         }

//         TextLog.Instance.Log("SubmitInput called");
//         StartCoroutine(SendDataToAPI(inputText));
//         TextLog.Instance.Log("Input Submitted: " + inputText);

//         isButtonClicked = false; // Reset the flag
//     }

//     IEnumerator SendDataToAPI(string data)
//     {
//         TextLog.Instance.Log("SendDataToAPI started.");
//         TextLog.Instance.Log("Current API URL: " + apiUrl);

//         if (string.IsNullOrEmpty(apiUrl))
//         {
//             TextLog.Instance.Log("Error: API URL is null or empty.");
//             yield break;
//         }

//         // Create the request
//         HTTPRequest request;
//         try
//         {
//             request = new HTTPRequest(new Uri(apiUrl), HTTPMethods.Post, (req, resp) =>
//             {
//                 TextLog.Instance.Log("HTTPRequest completed with state: " + req.State);

//                 if (resp != null)
//                 {
//                     TextLog.Instance.Log("Response received with status code: " + resp.StatusCode);
//                     TextLog.Instance.Log("Response message: " + resp.Message);
//                     TextLog.Instance.Log("Response data: " + resp.DataAsText);

//                     if (req.State == HTTPRequestStates.Finished)
//                     {
//                         if (resp.IsSuccess)
//                         {
//                             TextLog.Instance.Log("Upload finished successfully!");
//                             TextLog.Instance.Log("Response: " + resp.DataAsText);
//                         }
//                         else
//                         {
//                             TextLog.Instance.Log($"Server sent an error: {resp.StatusCode} - {resp.Message}");
//                             TextLog.Instance.Log($"Server response data: {resp.DataAsText}");
//                         }
//                     }
//                     else
//                     {
//                         TextLog.Instance.Log($"Request finished with error! Request state: {req.State}");
//                     }
//                 }
//             });

//             // Set up the request with JSON data
//             request.AddHeader("Content-Type", "application/json");
//             string jsonData = JsonUtility.ToJson(new { text = data });
//             request.RawData = System.Text.Encoding.UTF8.GetBytes(jsonData);
//             TextLog.Instance.Log("Request JSON data: " + jsonData);
//         }
//         catch (System.Exception ex)
//         {
//             TextLog.Instance.Log("HTTPRequest creation failed: " + ex.Message);
//             yield break;
//         }


//         // Wait for completion
//         yield return request;

//         switch (request.State)
//         {
//             case HTTPRequestStates.Finished:
//                 if (request.Response.IsSuccess)
//                 {
//                     TextLog.Instance.Log("Upload finished successfully!");
//                     TextLog.Instance.Log("Response: " + request.Response.DataAsText);
//                 }
//                 else
//                 {
//                     TextLog.Instance.Log($"Server sent an error: {request.Response.StatusCode} - {request.Response.Message}");
//                     TextLog.Instance.Log($"Server response data: {request.Response.DataAsText}");
//                 }
//                 break;

//             default:
//                 TextLog.Instance.Log($"Request finished with error! Request state: {request.State}");
//                 break;
//         }
//     }
// }
