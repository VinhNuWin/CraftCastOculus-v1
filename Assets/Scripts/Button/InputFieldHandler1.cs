using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldHandler : MonoBehaviour
{
    public InputField inputField;
    public GameObject virtualKeyboard;

    void Start()
    {
        // Ensure the virtual keyboard is inactive initially
        virtualKeyboard.SetActive(true);

        // Add listeners for select and deselect events using the Unity Event Wrapper
        // inputField.onEndEdit.AddListener(OnInputFieldDeselected);
    }

    public void OnInputFieldSelected()
    {
        TextLog.Instance.Log("Input field selected, activating virtual keyboard.");
        virtualKeyboard.SetActive(true);
        inputField.ActivateInputField(); // Keep the input field focused
    }

    public void OnClickKeyboard()
    {
        bool isActive = virtualKeyboard.activeSelf;
        virtualKeyboard.SetActive(!isActive);
        TextLog.Instance.Log($"Virtual keyboard toggled to {(isActive ? "inactive" : "active")}.");
    }

    // public void OnInputFieldDeselected(string text)
    // {
    //     TextLog.Instance.Log("Input field deselected.");
    //     virtualKeyboard.SetActive(false);
    // }

    // private void Update()
    // {
    //     if (inputField.isFocused)
    //     {
    //         if (!virtualKeyboard.activeSelf)
    //         {
    //             TextLog.Instance.Log("Input field is focused, ensuring virtual keyboard is active.");
    //             virtualKeyboard.SetActive(true);
    //         }
    //     }
    //     else
    //     {
    //         if (virtualKeyboard.activeSelf)
    //         {
    //             TextLog.Instance.Log("Input field is not focused, deactivating virtual keyboard.");
    //             virtualKeyboard.SetActive(false);
    //         }
    //     }
    // }
}
