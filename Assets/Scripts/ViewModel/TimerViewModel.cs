using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimerViewModel : MonoBehaviour
{
    public GameObject timerPrefab;
    public Transform timerPanel;
    private int currentTimerId = 0;
    public List<TimerModel> timers = new List<TimerModel>();
    public event Action<float, int> OnTimerChanged; // Include timerId
    public event Action<int> OnTimerCompleted;
    public static TimerViewModel Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        foreach (var timer in timers.ToArray()) // Use ToArray for safe iteration
        {
            if (!timer.timerActive) continue;

            timer.timeValue -= Time.deltaTime;
            OnTimerChanged?.Invoke(timer.timeValue, timer.timerId);

            if (timer.timeValue <= 0)
            {
                CompleteTimer(timer);
            }
        }
    }

    public void AddTimer(float newTimeValue, int associatedStepId)
    {
        if (timerPrefab == null)
        {
            TextLog.Instance.Log("Timer prefab is not assigned.");
            return;
        }

        GameObject timerGameObject = Instantiate(timerPrefab, timerPanel);
        TimerView timerViewComponent = timerGameObject.GetComponent<TimerView>();
        if (timerViewComponent != null)
        {
            timerViewComponent.SetTimer(newTimeValue, currentTimerId);
        }
        else
        {
            TextLog.Instance.Log("Failed to get TimerView component from instantiated timer prefab.");
        }

        TimerModel newTimer = new TimerModel(newTimeValue, true, currentTimerId, timerGameObject, associatedStepId);
        timers.Add(newTimer);
        currentTimerId++;
    }


    private void CompleteTimer(TimerModel timer)
    {
        OnTimerCompleted?.Invoke(timer.timerId);
    }


    public void RemoveTimerById(int id)
    {
        var timerToRemove = timers.Find(timer => timer.timerId == id);
        if (timerToRemove != null)
        {
            timers.Remove(timerToRemove);
            if (timerToRemove.timerGameObject != null)
            {
                Destroy(timerToRemove.timerGameObject);
            }
        }
    }

}