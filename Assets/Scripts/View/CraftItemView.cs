using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MyCraft.Models;

public class CraftItemView : MonoBehaviour
{
    private TextLog textLog;
    [SerializeField] private CraftViewModel viewModel;
    public Image craftImage;
    public Image authorImage;
    private Craft craft;
    private string craftId;
    public TextMeshProUGUI craftNameText, craftDetailsText, likesText, sharesText, dateText, authorText;
    public Button selectButton;

    void Awake()
    {
        FindAndAssignCraftViewModel();
    }

    // private void OnDestroy()
    // {
    //     selectButton.onClick.RemoveAllListeners();
    // }

    void FindAndAssignCraftViewModel()
    {
        viewModel = FindObjectOfType<CraftViewModel>();

        if (viewModel == null)
        {
            TextLog.Instance.Log("CraftViewModel not found. Ensure it exists in the current scene.");
        }
    }

    // UI List for Craft Feed
    public void Setup(Craft craft)
    {
        TextLog.Instance.Log("[CIV] Setup called");
        this.craft = craft;

        craftNameText.text = this.craft.Craft_Name;
        // likesText.text = this.craft.Likes.ToString();
        // sharesText.text = this.craft.Shares.ToString();
        authorText.text = this.craft.Craft_Author;
        if (craft.Post_Date.HasValue)
        {
            dateText.text = craft.Post_Date.Value.ToString("MMMM dd, yyyy");
        }
        else
        {
            dateText.text = "Date not available"; // Or any other placeholder text
        }

        LoadAndSetImage(craft.Craft_Image, craftImage);
        LoadAndSetImage(craft.Author_Image, authorImage);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            TextLog.Instance.Log($"Button clicked, selecting craft with ID: {this.craft.Craft_ID}");
            CraftDataPersist.Instance.SelectedCraft = craft;
        });
    }

    //UI selectCraft CraftUI
    public void UpdateSelectedCraftUI(Craft craft)
    {
        craftNameText.text = craft.Craft_Name;
        craftDetailsText.text = craft.Craft_Details;
        likesText.text = $"Likes: {craft.Likes}";
        sharesText.text = $"Shares: {craft.Shares}";
        craftImage.sprite = Resources.Load<Sprite>(craft.Craft_Image);
        authorImage.sprite = Resources.Load<Sprite>(craft.Author_Image);
    }

    public void LoadAndSetImage(string imagePath, Image targetImage)
    {
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        if (imageSprite != null)
        {
            targetImage.sprite = imageSprite;
            TextLog.Instance.Log("Image loaded successfully: " + imagePath);
        }
        else
        {
            // TextLog.Instance.Log("Image not found at path: " + imagePath);
        }
    }
}


