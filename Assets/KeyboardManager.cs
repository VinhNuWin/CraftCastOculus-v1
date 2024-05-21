// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using System;

// public class KeyboardManager : MonoBehaviour
// {
//     public OVRVirtualKeyboard virtualKeyboard;
//     public TMP_InputField tmpInputField;

//     private OVRVirtualKeyboard.KeyboardEventListener keyboardEventListener;

//     void Start()
//     {
//         if (virtualKeyboard == null)
//         {
//             virtualKeyboard = FindObjectOfType<OVRVirtualKeyboard>();
//         }

//         if (tmpInputField != null && virtualKeyboard != null)
//         {
//             InitializeKeyboard(virtualKeyboard, tmpInputField);
//         }
//         else
//         {
//             Debug.LogError("OVRVirtualKeyboard or TMP_InputField not found in the scene.");
//         }
//     }

//     private void InitializeKeyboard(OVRVirtualKeyboard keyboard, TMP_InputField tmpInput)
//     {
//         // Ensure the keyboard is initialized
//         if (keyboard.keyboardModelShader == null)
//         {
//             keyboard.keyboardModelShader = Shader.Find("Unlit/Color");
//         }

//         if (keyboard.keyboardModelAlphaBlendShader == null)
//         {
//             keyboard.keyboardModelAlphaBlendShader = Shader.Find("Unlit/Transparent");
//         }

//         if (FindObjectsOfType<OVRVirtualKeyboard>().Length > 1)
//         {
//             Debug.LogError("More than one instance of OVRVirtualKeyboard found.");
//             return;
//         }

//         if (keyboard.leftControllerDirectTransform == null && keyboard.leftControllerRootTransform != null)
//         {
//             if (keyboard.controllerDirectInteraction)
//             {
//                 Debug.LogWarning("Missing left controller direct transform for virtual keyboard input; falling back to the root!");
//             }
//             keyboard.leftControllerDirectTransform = keyboard.leftControllerRootTransform;
//         }

//         if (keyboard.rightControllerDirectTransform == null && keyboard.rightControllerRootTransform != null)
//         {
//             if (keyboard.controllerDirectInteraction)
//             {
//                 Debug.LogWarning("Missing right controller direct transform for virtual keyboard input; falling back to the root!");
//             }
//             keyboard.rightControllerDirectTransform = keyboard.rightControllerRootTransform;
//         }

//         if (OVRManager.instance)
//         {
//             keyboardEventListener = new OVRVirtualKeyboard.KeyboardEventListener(keyboard);
//             OVRManager.instance.RegisterEventListener(keyboardEventListener);
//         }

//         // Initialize serialized text commit field
//         keyboard.TextHandler = new OVRVirtualKeyboardTMPInputFieldTextHandler(tmpInput);

//         // Register for events
//         keyboard.CommitTextEvent.AddListener(OnCommitText);
//         keyboard.BackspaceEvent.AddListener(OnBackspace);
//         keyboard.EnterEvent.AddListener(OnEnter);
//         keyboard.KeyboardShownEvent.AddListener(OnKeyboardShown);
//         keyboard.KeyboardHiddenEvent.AddListener(OnKeyboardHidden);
//     }

//     private void OnCommitText(string text)
//     {
//         // Handle commit text logic
//         Debug.Log("Text committed: " + text);
//     }

//     private void OnBackspace()
//     {
//         // Handle backspace logic
//         Debug.Log("Backspace pressed");
//     }

//     private void OnEnter()
//     {
//         // Handle enter key logic
//         Debug.Log("Enter key pressed");
//     }

//     private void OnKeyboardShown()
//     {
//         // Handle keyboard shown logic
//         Debug.Log("Keyboard shown");
//     }

//     private void OnKeyboardHidden()
//     {
//         // Handle keyboard hidden logic
//         Debug.Log("Keyboard hidden");
//     }
// }

// public class OVRVirtualKeyboardTMPInputFieldTextHandler : OVRVirtualKeyboard.AbstractTextHandler
// {
//     private TMP_InputField tmpInputField;
//     private Action<string> onTextChanged;

//     public OVRVirtualKeyboardTMPInputFieldTextHandler(TMP_InputField tmpInputField)
//     {
//         this.tmpInputField = tmpInputField;
//         tmpInputField.onValueChanged.AddListener(HandleOnValueChanged);
//     }

//     public override Action<string> OnTextChanged
//     {
//         get => onTextChanged;
//         set => onTextChanged = value;
//     }

//     public override string Text => tmpInputField.text;
//     public override bool SubmitOnEnter => true;
//     public override bool IsFocused => tmpInputField.isFocused;

//     public override void Submit()
//     {
//         // Handle submit logic if needed
//     }

//     public override void AppendText(string s)
//     {
//         tmpInputField.text += s;
//         OnTextChanged?.Invoke(tmpInputField.text);
//     }

//     public override void ApplyBackspace()
//     {
//         if (tmpInputField.text.Length > 0)
//         {
//             tmpInputField.text = tmpInputField.text.Substring(0, tmpInputField.text.Length - 1);
//             OnTextChanged?.Invoke(tmpInputField.text);
//         }
//     }

//     public override void MoveTextEnd()
//     {
//         // Move caret to end of text
//         tmpInputField.caretPosition = tmpInputField.text.Length;
//     }

//     private void HandleOnValueChanged(string text)
//     {
//         OnTextChanged?.Invoke(text);
//     }
// }
