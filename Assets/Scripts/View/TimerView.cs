using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerView : MonoBehaviour
{
    private TextLog textLog; // Make sure TextLog is properly implemented
    public TextMeshProUGUI timerText;

    [SerializeField]
    private Button timerButton;
    private float timeRemaining;
    private bool timerIsActive = false;
    private string timerId; // Changed from int to string

    public event Action<string> OnTimerCompleted; // This already expects a string, which is good

    private void Start()
    {
        if (timerButton != null)
        {
            timerButton.onClick.AddListener(OnTimerButtonClick);
            timerButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!timerIsActive) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining, timerId);
        }
        else
        {
            timerIsActive = false;
            TimerCompleted(timerId);
        }
    }

    public void SetTimer(float time, string timerId)
    {
        this.timeRemaining = time;
        this.timerId = timerId;
        this.timerIsActive = true;

        UpdateTimerDisplay(time, timerId);
        timerText.gameObject.SetActive(true);
        timerButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        TimerViewModel.Instance.OnTimerChanged += UpdateTimerDisplay;
        TimerViewModel.Instance.OnTimerCompleted += TimerCompleted;
    }

    private void OnDisable()
    {
        if (TimerViewModel.Instance != null)
        {
            TimerViewModel.Instance.OnTimerChanged -= UpdateTimerDisplay;
            TimerViewModel.Instance.OnTimerCompleted -= TimerCompleted;
        }
    }

    private void UpdateTimerDisplay(float time, string id)
    {
        if (id != this.timerId) return;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TimerCompleted(string id)
    {
        if (id != this.timerId) return;

        timerIsActive = false;
        timerText.gameObject.SetActive(false);
        timerButton.gameObject.SetActive(true);
    }

    private void OnTimerButtonClick()
    {
        TimerViewModel.Instance.RemoveTimerById(timerId);
    }
}
