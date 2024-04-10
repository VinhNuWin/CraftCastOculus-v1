using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{

    private TextLog textLog;
    public GameObject CraftSelectPanel;
    public GameObject dimPanel;
    // public DimmingControl dimmingControl;

    void Awake()
    {
        DOTween.Init();
        TextLog.Instance.Log("[CM] initialized");
        CraftDataPersist.Instance.OnCraftSelected += HandleCraftSelected;

        // craftViewModel.OnCraftSelected += (craft) =>
        // {
        //     TextLog.Instance.Log($"Inline subscription test: Craft selected with ID {craft.Craft_ID}");
        // };
    }

    private void OnDestroy()
    {
        if (CraftDataPersist.Instance != null)
        {
            CraftDataPersist.Instance.OnCraftSelected -= HandleCraftSelected;
        }
    }

    private void HandleCraftSelected(Craft selectedCraft)
    {
        TextLog.Instance.Log("[CM] HandleCraftSelected initialized");
        // DimPrimaryCanvas(true);
        ShowCraftDetailsOverlay(true);
    }

    // private void DimPrimaryCanvas(bool shouldDim)
    // {
    //     if (dimPanel != null)
    //     {
    //         TextLog.Instance.Log("[DimPrimaryCanvas] Dim HomeFeed Panel");
    //         var canvasGroup = dimPanel.GetComponent<CanvasGroup>();
    //         if (canvasGroup == null) // Ensure there's a CanvasGroup component
    //         {
    //             canvasGroup = dimPanel.AddComponent<CanvasGroup>();
    //         }

    //         float targetAlpha = shouldDim ? 1.0f : 0.0f; // Assuming 1 is fully visible (dimmed) and 0 is fully transparent
    //         float duration = 0.5f; // Duration of the dimming effect, in seconds

    //         canvasGroup.DOFade(targetAlpha, duration).SetUpdate(true);
    //     }
    // }

    private void ShowCraftDetailsOverlay(bool showDetails)
    {
        TextLog.Instance.Log("[ShowCraftDetailsOverlay] setting overlay to true");
        if (CraftSelectPanel != null)
        {
            CraftSelectPanel.SetActive(showDetails);
        }
    }
}