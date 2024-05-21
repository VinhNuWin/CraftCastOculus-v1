using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Web;
using System.Text;
using MyCraft.Models;

public class DeepLinkHandler : MonoBehaviour
{
    void Awake()
    {
        TextLog.Instance.Log("DeepLink Subscribed");
        // Subscribe to deep link event
        Application.deepLinkActivated += HandleDeepLink;

        // Handle any deep link passed when the app starts
        if (!string.IsNullOrEmpty(Application.absoluteURL))
        {
            HandleDeepLink(Application.absoluteURL);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        Application.deepLinkActivated -= HandleDeepLink;
    }

    void HandleDeepLink(string url)
    {
        Debug.Log($"Received deep link: {url}");
        TextLog.Instance.Log("Received deep link");

        try
        {
            // Extract the query parameters from the URL
            Uri uri = new Uri(url);
            var queryParameters = HttpUtility.ParseQueryString(uri.Query);
            string sceneId = queryParameters["sceneId"];
            string data = queryParameters["data"];

            if (!string.IsNullOrEmpty(sceneId))
            {
                LoadScene(sceneId);
            }

            if (!string.IsNullOrEmpty(data))
            {
                ProcessCraftData(HttpUtility.UrlDecode(data));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to handle deep link: {ex.Message}");
        }
    }

    void LoadScene(string sceneId)
    {
        // If sceneId is numeric, you can handle it as an index
        if (int.TryParse(sceneId, out int sceneIndex))
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        else
        {
            // Otherwise, treat it as a scene name
            SceneManager.LoadSceneAsync(sceneId);
        }
    }

    void ProcessCraftData(string jsonData)
    {
        Debug.Log("Received JSON Data: " + jsonData);
        try
        {
            Craft craft = JsonUtility.FromJson<Craft>(jsonData);
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
                Debug.LogError("Failed to parse Craft from JSON data");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing craft data: " + ex.Message);
        }
    }
}

// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System;
// using System.Web;

// public class DeepLinkHandler : MonoBehaviour
// {
//     void Awake()
//     {
//         TextLog.Instance.Log($"DeepLink Subscribed");
//         // Subscribe to deep link event
//         Application.deepLinkActivated += HandleDeepLink;

//         // Handle any deep link passed when the app starts
//         if (!string.IsNullOrEmpty(Application.absoluteURL))
//         {
//             HandleDeepLink(Application.absoluteURL);
//         }
//     }

//     private void OnDestroy()
//     {
//         // Unsubscribe when the object is destroyed
//         Application.deepLinkActivated -= HandleDeepLink;
//     }

//     void HandleDeepLink(string url)
//     {
//         Debug.Log($"Received deep link: {url}");
//         TextLog.Instance.Log($"Received deep link");

//         try
//         {
//             // Extract the query parameters from the URL
//             Uri uri = new Uri(url);
//             var queryParameters = HttpUtility.ParseQueryString(uri.Query);
//             string sceneId = queryParameters["sceneId"];

//             if (!string.IsNullOrEmpty(sceneId))
//             {
//                 LoadScene(sceneId);
//             }
//             else
//             {
//                 Debug.LogWarning("No scene ID provided in the deep link URL.");
//             }
//         }
//         catch (Exception ex)
//         {
//             Debug.LogError($"Failed to handle deep link: {ex.Message}");
//         }
//     }

//     void LoadScene(string sceneId)
//     {
//         // If sceneId is numeric, you can handle it as an index
//         if (int.TryParse(sceneId, out int sceneIndex))
//         {
//             SceneManager.LoadSceneAsync(sceneIndex);
//         }
//         else
//         {
//             // Otherwise, treat it as a scene name
//             SceneManager.LoadSceneAsync(sceneId);
//         }
//     }
// }
