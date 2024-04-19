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

    private void ShowCraftDetailsOverlay(bool showDetails)
    {
        TextLog.Instance.Log("[ShowCraftDetailsOverlay] setting overlay to true");
        if (CraftSelectPanel != null)
        {
            CraftSelectPanel.SetActive(showDetails);
        }
    }
}