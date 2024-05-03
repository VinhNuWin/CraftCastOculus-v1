using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmCraftPokeInteractable : MonoBehaviour
{
    // Reference to the CanvasGroup that should be enabled/disabled
    public CanvasGroup pokeInteractable;

    void Start()
    {
        DelayedVisibilitySetup();
    }

    IEnumerator DelayedVisibilitySetup()
    {
        yield return new WaitForSeconds(0.1f); // Adjust time as needed
        if (pokeInteractable != null)
        {
            pokeInteractable.alpha = 0;
            pokeInteractable.interactable = false;
            pokeInteractable.blocksRaycasts = false;
        }
    }



    private void OnEnable()
    {
        // Subscribe to the OnCraftSelected event
        CraftDataPersist.Instance.OnCraftSelected += OnCraftSelected;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks or unwanted behavior when the script is disabled
        CraftDataPersist.Instance.OnCraftSelected -= OnCraftSelected;
    }

    private void OnCraftSelected(Craft craft)
    {
        // Check if a valid craft is selected
        if (craft != null && pokeInteractable != null)
        {
            // Log the selection (optional, for debugging)
            Debug.Log($"Craft selected: {craft.Craft_Name}, activating poke interactable.");

            // Activate the pokeInteractable by making it visible and interactable
            pokeInteractable.alpha = 1;
            pokeInteractable.interactable = true;
            pokeInteractable.blocksRaycasts = true;

            // Optionally, trigger any animations or effects
            FadeInEffect fadeInEffect = pokeInteractable.GetComponent<FadeInEffect>();
            if (fadeInEffect != null)
            {
                fadeInEffect.FadeInUIElement();
            }
        }
        else
        {
            // If no valid craft is selected, ensure the pokeInteractable is inactive
            if (pokeInteractable != null)
            {
                pokeInteractable.alpha = 0;
                pokeInteractable.interactable = false;
                pokeInteractable.blocksRaycasts = false;
            }
            Debug.Log("No craft selected or craft is null, poke interactable remains inactive.");
        }
    }
}
