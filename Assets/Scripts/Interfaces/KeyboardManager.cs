// using UnityEngine;
// using UnityEngine.UI;
// using Oculus.Input.Input;
// using TMPro;

// public class KeyboardManager : MonoBehaviour
// {
//     public TMP_InputField inputField;
//     public GameObject keyboard; // Drag your keyboard GameObject here in the inspector

//     void Start()
//     {
//         // Initially hide the keyboard
//         KeyboardProvider.Close();
//     }

//     // Call this method when the input field is selected
//     public void ShowKeyboard()
//     {
//         if (inputField != null)
//         {
//             KeyboardProvider.Open(inputField); // Open the keyboard with the input field as the target
//         }
//     }

//     // Optional: call this method to hide the keyboard when input is deselected
//     public void HideKeyboard()
//     {
//         KeyboardProvider.Close();
//     }
// }
