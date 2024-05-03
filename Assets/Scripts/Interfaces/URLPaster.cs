using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class URLPaster : MonoBehaviour
{
    public TMP_InputField tmpInputField;
    public UnityEvent onPoke;

    void Start()
    {
        // Subscribe to the UnityEvent
        if (onPoke == null)
            onPoke = new UnityEvent();

        onPoke.AddListener(PasteURL);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the UnityEvent to clean up
        onPoke.RemoveListener(PasteURL);
    }

    private void PasteURL()
    {
        // Check if the clipboard contains text that can be pasted
        if (GUIUtility.systemCopyBuffer != null)
        {
            tmpInputField.text = GUIUtility.systemCopyBuffer;
            Debug.Log("URL Pasted: " + GUIUtility.systemCopyBuffer);
        }
    }

    // This method can be called from any other script to trigger the paste operation
    public void TriggerPoke()
    {
        onPoke.Invoke();
    }
}
