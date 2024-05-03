using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For Button
using DG.Tweening;

public class HomeUIManager : MonoBehaviour
{
    public GameObject panel;        // Assign panel GameObject in the inspector
    public GameObject loadingText;  // Assign loadingText GameObject in the inspector

    public Button apiCallButton;

    void Start()
    {
        // Assuming your button is already linked through the Inspector,
        // add the onClick listener like this:
        apiCallButton.onClick.AddListener(() =>
         {
             HidePanelAndShowLoading();
         });
    }

    void HidePanelAndShowLoading()
    {
        // Hide panel with fade and scale animation
        panel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        panel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() =>
        {
            panel.SetActive(false);
            loadingText.SetActive(true);
            loadingText.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        });
    }
}
