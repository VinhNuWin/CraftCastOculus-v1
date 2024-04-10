using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    private TextLog textLog;
    public TextMeshProUGUI timerText;

    [SerializeField]
    private Button timerButton;
    private float timeRemaining;
    private bool timerIsActive = false;
    private int timerId;

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

    public void SetTimer(float time, int id)
    {
        this.timeRemaining = time;
        this.timerId = id;
        this.timerIsActive = true;

        // Optionally reset and show the timer UI here
        UpdateTimerDisplay(time, id);
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

    private void UpdateTimerDisplay(float time, int id)
    {
        if (id != this.timerId) return;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TimerCompleted(int id)
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
