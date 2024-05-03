using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus.Interaction;
using DG.Tweening;


public class CraftSceneTransitionOnPoke : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels; // Assign all panels to fade out in the inspector

    [SerializeField]
    private InteractableUnityEventWrapper eventWrapper;

    [SerializeField]
    private float delayBeforeLoading = 1.5f; // Adjust based on your animation duration

    private void Start()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.AddListener(TransitionToCraftScene);
        }
    }

    private void TransitionToCraftScene()
    {
        StartCoroutine(StaggeredFadeOutAndLoadScene());
    }

    private IEnumerator StaggeredFadeOutAndLoadScene()
    {
        float delay = 0.0f;
        foreach (GameObject panel in panels)
        {
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(0, 0.5f).SetDelay(delay);
                panel.transform.DOMoveY(panel.transform.position.y - 100, 0.5f).SetDelay(delay).SetEase(Ease.InQuad);
            }
            delay += 0.1f; // Stagger delay for each panel
        }

        // Wait for the last animation to finish plus any additional delay you want before loading the scene
        yield return new WaitForSeconds(delay + delayBeforeLoading);

        // Load the scene
        SceneManager.LoadScene("CraftScene");
    }

    private void OnDestroy()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.RemoveListener(TransitionToCraftScene);
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;
// using Oculus.Interaction;

// public class CraftSceneTransitionOnPoke : MonoBehaviour
// {
//     private TextLog textLog;

//     [SerializeField]
//     private InteractableUnityEventWrapper eventWrapper;

//     private void Start()
//     {
//         if (eventWrapper != null)
//         {
//             eventWrapper.WhenSelect.AddListener(TransitionToCraftScene);
//         }
//     }

//     private void TransitionToCraftScene()
//     {
//         TextLog.Instance.Log("[Scene] LoadScene CraftScene");
//         SceneManager.LoadScene("CraftScene");
//     }

//     private void OnDestroy()
//     {
//         if (eventWrapper != null)
//         {
//             eventWrapper.WhenSelect.RemoveListener(TransitionToCraftScene);
//         }
//     }
// }