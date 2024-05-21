using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardInput : MonoBehaviour
{
    public InputField inputField;
    public GameObject keyboard;

    void Start()
    {
        // Add EventTrigger component to the input field if it doesn't already exist
        EventTrigger trigger = inputField.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = inputField.gameObject.AddComponent<EventTrigger>();
        }

        // Create and add OnSelect event entry
        EventTrigger.Entry selectEntry = new EventTrigger.Entry();
        selectEntry.eventID = EventTriggerType.Select;
        selectEntry.callback.AddListener((eventData) => { OnInputFieldSelected(); });
        trigger.triggers.Add(selectEntry);

        // Create and add OnDeselect event entry (optional)
        EventTrigger.Entry deselectEntry = new EventTrigger.Entry();
        deselectEntry.eventID = EventTriggerType.Deselect;
        deselectEntry.callback.AddListener((eventData) => { OnInputFieldDeselected(); });
        trigger.triggers.Add(deselectEntry);
    }

    void OnInputFieldSelected()
    {
        // Show the keyboard
        keyboard.SetActive(true);
    }

    void OnInputFieldDeselected()
    {
        // Optionally hide the keyboard
        keyboard.SetActive(false);
    }

    // Add more methods to handle keyboard input and update the input field
    public void OnKeyPress(string key)
    {
        inputField.text += key;
    }

    public void OnBackspace()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void OnEnter()
    {
        // Handle enter key press
        keyboard.SetActive(false);
    }

}
