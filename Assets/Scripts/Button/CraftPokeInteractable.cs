using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class CraftPokeInteractable : MonoBehaviour, IPokeInteractable
{
    public UnityEvent OnPoke;

    [SerializeField]
    private InteractableUnityEventWrapper eventWrapper;

    private void Start()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.AddListener(HandleSelect);
        }
    }

    public void SimulatePoke()
    {
        // Directly invoke the OnPoke event to simulate a poke
        OnPoke?.Invoke();
    }

    private void HandleSelect()
    {
        // Invoke the OnPoke event when a poke (select in this context) is detected
        OnPoke?.Invoke();
        TextLog.Instance.Log("[CraftSelect] Select Poke triggered, showing popup");
    }

    private void OnDestroy()
    {
        // Clean up event subscription
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.RemoveListener(HandleSelect);
        }
    }
}
