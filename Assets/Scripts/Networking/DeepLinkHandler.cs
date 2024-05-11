using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Web;

public class DeepLinkHandler : MonoBehaviour
{
    void Awake()
    {
        TextLog.Instance.Log($"DeepLink Subscribed");
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
        TextLog.Instance.Log($"Received deep link");

        try
        {
            // Extract the query parameters from the URL
            Uri uri = new Uri(url);
            var queryParameters = HttpUtility.ParseQueryString(uri.Query);
            string sceneId = queryParameters["sceneId"];

            if (!string.IsNullOrEmpty(sceneId))
            {
                LoadScene(sceneId);
            }
            else
            {
                Debug.LogWarning("No scene ID provided in the deep link URL.");
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
}
