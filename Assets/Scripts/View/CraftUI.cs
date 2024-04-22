using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CraftUI : MonoBehaviour
{
    private TextLog textLog;
    private Craft craft;
    public TextMeshProUGUI craftNameText, craftDetailsText, craftAuthor, craftCategory, postDate;
    public Image craftImage;
    public Image authorImage;
    // public Button selectButton;
    public Sprite defaultImageSprite;
    public Sprite defaultErrorSprite;
    [SerializeField]
    private CraftPokeInteractable craftSelectPokeInteractable;

    // void Start()
    // {
    //     craftSelectPokeInteractable = GetComponentInChildren<CraftPokeInteractable>(true);
    //     if (craftSelectPokeInteractable == null)
    //     {
    //         Debug.LogError("CraftPokeInteractable not found on the GameObject or its children.");
    //     }
    // }

    public void Setup(Craft craft)
    {
        this.craft = craft;
        try
        {
            // TextLog.Instance.Log($"[CraftUI] Updating UI of craft with ID: {craft.Craft_ID}");

            if (craftNameText != null) craftNameText.text = this.craft.Craft_Name;
            if (craftDetailsText != null) craftDetailsText.text = this.craft.Craft_Details;
            if (craftAuthor != null) craftAuthor.text = this.craft.Craft_Author;
            if (craftCategory != null) craftCategory.text = this.craft.Category;
            // if (postDate != null) postDate.text = this.craft.Post_Date.ToString("MMMM dd, yyyy");

            if (!string.IsNullOrEmpty(craft.Craft_Image))
            {
                LoadAndSetImage(craft.Craft_Image, craftImage);
            }

            if (!string.IsNullOrEmpty(craft.Author_Image) && authorImage != null)
            {
                LoadAndSetImage(craft.Author_Image, authorImage);
            }
            else
            {
                // Optionally handle cases where there is no author image or the image component is not assigned
                if (authorImage != null) authorImage.sprite = defaultImageSprite; // Set to default if no author image provided
            }

            craftSelectPokeInteractable = GetComponentInChildren<CraftPokeInteractable>(true);

            if (craftSelectPokeInteractable != null)
            {
                craftSelectPokeInteractable.OnPoke.RemoveAllListeners();  // Clear previous listeners
                craftSelectPokeInteractable.OnPoke.AddListener(() =>
                {
                    if (this.craft != null) // Additional safety check
                    {
                        TextLog.Instance.Log($"[CraftUI] Attempting to set selected craft: {this.craft.Craft_ID} successful");
                        CraftDataPersist.Instance.SelectedCraft = this.craft;
                    }
                });
            }


        }
        catch (Exception ex)
        {
            TextLog.Instance.Log($"[CraftUI] Error in Setup method: {ex.Message}");
            // Optionally, log the stack trace for more detailed debugging information.
            Debug.LogError($"Stack Trace: {ex.StackTrace}");
        }
    }

    private void OnCraftSelect()
    {
        TextLog.Instance.Log($"Craft selected: {this.craft.Craft_Name}");
    }



    public void LoadAndSetImage(string imagePath, Image targetImage)
    {
        try
        {
            Sprite imageSprite = Resources.Load<Sprite>(imagePath);
            if (imageSprite != null)
            {
                targetImage.sprite = imageSprite;
                // TextLog.Instance.Log("Image loaded successfully: " + imagePath);
            }
            else
            {
                // TextLog.Instance.Log("Image not found at path: " + imagePath);
                // Optionally, set a default image if the intended image cannot be loaded
                targetImage.sprite = defaultImageSprite;
            }
        }
        catch (Exception ex)
        {
            TextLog.Instance.Log($"Failed to load image at path: {imagePath} with exception: {ex.Message}");
            // Handling the error here allows the loop to continue despite this failure.

            // Consider setting the image to a default error image, logging the error, etc.
            targetImage.sprite = defaultErrorSprite;
        }
    }
}