// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class CanvasController : MonoBehaviour, IPointerClickHandler
// {
//     private TextLog textLog;
//     public bool IsCraftSelectionActive { get; private set; } = false;
//     public static CanvasController Instance { get; private set; }
//     // public CanvasGroup homePageCanvasGroup;
//     public CanvasGroup craftSelectCanvasGroup;
//     public float fadeDuration = 0.5f;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Make this object persistent
//             CraftDataPersist.Instance.OnCraftSelected += HandleCraftSelected;
//             // TextLog.Instance.Log("CanvasController initialized");
//         }
//         else if (Instance != this)
//         {
//             Destroy(gameObject); // Ensure there's only one instance in the scene
//         }
//     }

//     private void Start()
//     {
//         // Initially, the HomePageCanvas is visible, and the CraftSelectCanvas is invisible.
//         // homePageCanvasGroup.alpha = 1.0f;
//         craftSelectCanvasGroup.alpha = 0.0f;
//         craftSelectCanvasGroup.interactable = false;
//         craftSelectCanvasGroup.blocksRaycasts = false;

//         // TextLog.Instance.Log("CraftSelectCanvas interactable: " + backgroundClickDetector.interactable);
//         // TextLog.Instance.Log("CraftSelect invisible/ Not interactable & blocking raycasts && homepage visible");
//     }

//     private void OnDestroy()
//     {
//         // Corrected to match the subscription in Awake
//         if (CraftDataPersist.Instance != null)
//         {
//             CraftDataPersist.Instance.OnCraftSelected -= HandleCraftSelected;
//         }
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//         // Simple toggle logic as an example
//         if (!IsCraftSelectionActive)
//         {
//             // Assume this method will show the craft selection UI
//             ShowCraftSelection();
//         }
//         else
//         {
//             // Hide the craft selection UI and show the home page
//             ShowHomePageFeed();
//         }
//     }

//     private void HandleCraftSelected(Craft craft)
//     {
//         // // Implement what happens when a craft is selected
//         // TextLog.Instance.Log("OnCraft Selected Toggle Background becomes clickable && CraftSelectCanvas visible");

//         ShowCraftSelection(craft);
//     }

//     private void ShowCraftSelection(Craft craft)
//     {
//         IsCraftSelectionActive = true;
//         craftSelectCanvasGroup.DOFade(1.0f, fadeDuration);
//         craftSelectCanvasGroup.interactable = true;
//         craftSelectCanvasGroup.blocksRaycasts = true;

//         // homePageCanvasGroup.DOFade(0.0f, fadeDuration).SetUpdate(true);
//         // homePageCanvasGroup.interactable = false;

//         // TextLog.Instance.Log("CraftSelectCanvas interactable: " + backgroundClickDetector.interactable);
//         // TextLog.Instance.Log("CraftSelect visible/interactable/blocksraycasts && homepage invisible");
//     }

//     private void ShowCraftSelection()
//     {
//         IsCraftSelectionActive = true;
//         craftSelectCanvasGroup.DOFade(1.0f, fadeDuration);
//         craftSelectCanvasGroup.interactable = true;
//         craftSelectCanvasGroup.blocksRaycasts = true;

//         // homePageCanvasGroup.DOFade(0.0f, fadeDuration).SetUpdate(true);
//     }

//     public void ShowHomePageFeed()
//     {
//         // homePageCanvasGroup.DOFade(1.0f, fadeDuration);
//         // // homePageCanvasGroup.interactable = true;

//         IsCraftSelectionActive = false;
//         // homePageCanvasGroup.DOFade(1.0f, fadeDuration).SetUpdate(true);
//         craftSelectCanvasGroup.DOFade(0.0f, fadeDuration).SetUpdate(true);
//         craftSelectCanvasGroup.interactable = false;
//         craftSelectCanvasGroup.blocksRaycasts = false;

//         // TextLog.Instance.Log("CraftSelectCanvas interactable: " + craftSelectCanvasGroup.interactable);
//         // TextLog.Instance.Log("CraftSelect invisible && homepage visible");
//     }
// }