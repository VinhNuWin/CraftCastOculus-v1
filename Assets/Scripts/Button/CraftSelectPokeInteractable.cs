using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class CraftSelectPokeInteractable : MonoBehaviour
{
    public UnityEvent OnPoke;

    [SerializeField]
    private InteractableUnityEventWrapper eventWrapper;

    private void Start()
    {
        if (eventWrapper != null)
        {
            // Subscribe to the appropriate events from the event wrapper
            // Assuming these events exist and are relevant to your use case
            eventWrapper.WhenSelect.AddListener(HandlePokeDetected);
            // Depending on the SDK version, you might need to use different events
        }
    }

    private void HandlePokeDetected()
    {
        // Invoke the OnPoke event when a poke (select in this context) is detected
        OnPoke?.Invoke();
    }

    private void OnDestroy()
    {
        // Clean up event subscription
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.RemoveListener(HandlePokeDetected);
        }
    }
}
