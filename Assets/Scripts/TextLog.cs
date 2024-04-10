using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        }
        else
        {
            Debug.LogWarning("TextLog component not assigned in the inspector.");
        }
    }
}