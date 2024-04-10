using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Oculus.Interaction;

public class CraftSceneTransitionOnPoke : MonoBehaviour
{
    private TextLog textLog;

    [SerializeField]
    private InteractableUnityEventWrapper eventWrapper;

    private void Start()
    {
        if (eventWrapper != null)
        {
            eventWrapper.WhenSelect.AddListener(TransitionToCraftScene);
        }
    }

    private void TransitionToCraftScene()
    {
        TextLog.Instance.Log("[Scene] LoadScene CraftScene");
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