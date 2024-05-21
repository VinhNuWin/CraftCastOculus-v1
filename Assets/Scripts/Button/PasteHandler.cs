using UnityEngine;
using UnityEngine.UI;

public class PasteHandler : MonoBehaviour
{
    public InputField inputField;

    void Update()
    {
        if (inputField == null)
        {
            // Debug.LogError("InputField is not assigned.");
            return;
        }

        // Check if the input field is focused and the designated controller button is pressed
        if (inputField.isFocused)
        {
            // Debug.Log("InputField is focused.");

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                // Debug.Log("Controller button pressed.");
                PasteText();
            }
        }
        else
        {
            // Debug.Log("InputField is not focused.");
        }
    }

    void PasteText()
    {
        string clipboardText = GUIUtility.systemCopyBuffer;
        if (!string.IsNullOrEmpty(clipboardText))
        {
            Debug.Log("Pasting text: " + clipboardText);
            inputField.text += clipboardText;
        }
        else
        {
            Debug.Log("Clipboard is empty.");
        }
    }
}
