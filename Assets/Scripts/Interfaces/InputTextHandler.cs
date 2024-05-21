using UnityEngine;
using System;
using TMPro;  // Include the TextMeshPro namespace

public class InputTextHandler : OVRVirtualKeyboard.AbstractTextHandler
{
    [SerializeField] private TMP_InputField inputField;  // Use TMP_InputField instead of InputField

    public override Action<string> OnTextChanged { get; set; }

    public override string Text => inputField.text;

    public override bool SubmitOnEnter => true;

    public override bool IsFocused => inputField.isFocused;

    public override void Submit()
    {
        // Submit the text or close the keyboard
    }

    public override void AppendText(string s)
    {
        inputField.text += s;
        OnTextChanged?.Invoke(inputField.text);
    }

    public override void ApplyBackspace()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            OnTextChanged?.Invoke(inputField.text);
        }
    }

    public override void MoveTextEnd()
    {
        // Moves the cursor to the end of the text in the TMP_InputField
        inputField.MoveTextEnd(false);
    }
}
