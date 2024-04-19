using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this line

public class TextLog : MonoBehaviour
{
    public static TextLog Instance { get; private set; }
    public TextMeshProUGUI debugTextUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persist across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy any duplicates
        }
    }

    public void Log(string message)
    {
        if (debugTextUI != null)
        {
            debugTextUI.text += message + "\n";
            // Ensure the canvas updates and scrolls to the bottom
            Canvas.ForceUpdateCanvases(); // Update the Canvas immediately
            debugTextUI.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0f; // Scroll to bottom
        }
        else
        {
            Debug.LogWarning("TextLog component not assigned in the inspector.");
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class TextLog : MonoBehaviour
// {
//     public static TextLog Instance { get; private set; }
//     public TextMeshProUGUI debugTextUI;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Make this GameObject persist across scenes
//         }
//         else if (Instance != this)
//         {
//             Destroy(gameObject); // Destroy any duplicates
//         }
//     }

//     public void Log(string message)
//     {
//         if (debugTextUI != null)
//         {
//             debugTextUI.text += message + "\n";
//         }
//         else
//         {
//             Debug.LogWarning("TextLog component not assigned in the inspector.");
//         }
//     }
// }