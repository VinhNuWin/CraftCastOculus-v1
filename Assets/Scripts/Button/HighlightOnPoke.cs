using UnityEngine;

public class HighlightOnPoke : MonoBehaviour
{
    public Color highlightColor = Color.yellow;  // Color when highlighted
    private Color originalColor;                 // To store the original color
    private Renderer rend;                       // Renderer to change the color

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color; // Save the original color
        }
    }

    // Method to be called when the panel is poked
    public void OnPoke()
    {
        if (rend != null)
        {
            rend.material.color = highlightColor; // Change to highlight color
        }
    }

    // Method to revert the color when the poke is released or another condition met
    public void OnRelease()
    {
        if (rend != null)
        {
            rend.material.color = originalColor; // Revert to the original color
        }
    }

    // Example interaction detection
    private void OnTriggerEnter(Collider other)
    {
        // Assuming a specific tag for what can trigger the highlight. Adjust as necessary.
        if (other.CompareTag("PlayerHand"))
        {
            OnPoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            OnRelease();
        }
    }
}
