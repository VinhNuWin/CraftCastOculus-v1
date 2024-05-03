// using UnityEngine;
// using UnityEngine.EventSystems;

// public class BackgroundClickDetector : MonoBehaviour, IPointerClickHandler
// {
//     [SerializeField]
//     public CanvasController canvasController;
//     private TextLog textLog;
//     private bool canClickOverlay = false; // Initially, clicking is disabled

//     public void SetCanClickOverlay(bool canClick)
//     {
//         canClickOverlay = canClick;
//         TextLog.Instance.Log("Background panel is now " + (canClick ? "clickable." : "not clickable."));
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//         TextLog.Instance.Log("Background Canvas Clicked");

//         canvasController.ShowHomePageFeed();
//     }


// }