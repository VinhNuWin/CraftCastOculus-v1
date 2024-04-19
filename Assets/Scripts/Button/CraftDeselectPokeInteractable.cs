using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Oculus.Interaction;

public class CraftDeselectPokeInteractable : MonoBehaviour, IPokeInteractable
{
    private TextLog textLog;
    public UnityEvent OnPoke;

    [SerializeField]
    private InteractableUnityEventWrapper eventWrapper;

    private void Start()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.AddListener(HandleCraftDeselect);
        }
    }

    public void SimulatePoke()
    {
        // Directly invoke the OnPoke event to simulate a poke
        OnPoke?.Invoke();
    }

    private void HandleCraftDeselect()
    {
        OnPoke?.Invoke();
        TextLog.Instance.Log("[CraftSelect] Deselect Poke triggered, hiding popup");
    }

    private void OnDestroy()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.RemoveListener(HandleCraftDeselect);
        }
    }
}
