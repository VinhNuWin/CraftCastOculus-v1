using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSimulator : MonoBehaviour
{
    [Tooltip("Key to press for simulating the poke interaction.")]
    public KeyCode triggerKey = KeyCode.Space;

    [Tooltip("Target GameObject to receive the method call.")]
    public GameObject targetObject;

    private IPokeInteractable pokeInteractable;

    private void Start()
    {
        // Attempt to get the IPokeInteractable component from the target object
        if (targetObject != null)
        {
            pokeInteractable = targetObject.GetComponent<IPokeInteractable>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey) && pokeInteractable != null)
        {
            // Call SimulatePoke on the interface
            pokeInteractable.SimulatePoke();
        }
    }
}
