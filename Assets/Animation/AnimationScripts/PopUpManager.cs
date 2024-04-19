using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PopupManager : MonoBehaviour
{
    private TextLog textLog;
    public GameObject selectedCraftWindow; // Make sure this is set in the Inspector
    private Animator animator;  // Animator attached to the popup panel

    private static PopupManager instance;
    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PopupManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PopupManager");
                    instance = obj.AddComponent<PopupManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        animator = selectedCraftWindow.GetComponent<Animator>();
        if (animator == null)
        {
            TextLog.Instance.Log("Failed to find Animator on selectedCraftWindow");
        }
        HidePopup();
    }

    public void ShowPopup()
    {
        if (animator != null)
        {
            animator.SetBool("IsVisible", true);
            TextLog.Instance.Log("[PopM] ShowPopup triggered");
        }
        else
        {
            TextLog.Instance.Log("Animator not available when attempting to show popup.");
        }
    }

    public void HidePopup()
    {
        if (animator != null)
        {
            animator.SetBool("IsVisible", false);
            TextLog.Instance.Log("[PopM] HidePopup triggered");
        }
        else
        {
            TextLog.Instance.Log("Animator not available when attempting to hide popup.");
        }
    }
}
