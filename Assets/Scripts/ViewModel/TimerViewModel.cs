using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimerViewModel : MonoBehaviour
{
    public GameObject timerPrefab;
    // public Transform timerPanel;
    public List<TimerModel> timers = new List<TimerModel>();
    public event Action<float, string> OnTimerChanged;
    public event Action<string> OnTimerCompleted;
    public static TimerViewModel Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            TextLog.Instance.Log("TimerViewModel Instantiated");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        foreach (var timer in timers.ToArray())
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

    // public void AddTimer(float newTimeValue, Vector3 position, Quaternion rotation, string stepId)
    // {
    //     TextLog.Instance.Log("TimerViewModel add timer reached");
    //     if (timerPrefab == null)
    //     {
    //         TextLog.Instance.Log("Timer prefab is not assigned.");
    //         return;  // Make sure this isn't happening
    //     }

    //     // Further code...
    // }


    public void AddTimer(float newTimeValue, Vector3 position, Quaternion rotation, string stepId)
    {
        TextLog.Instance.Log("TimerViewModel add timer reached");
        if (timerPrefab == null)
        {
            TextLog.Instance.Log("Timer prefab is not assigned.");
            return;
        }

        TextLog.Instance.Log("TimerViewModel: Instantiating timer prefab.");
        GameObject timerGameObject = Instantiate(timerPrefab, position, rotation);
        TextLog.Instance.Log("TimerViewModel: Prefab instantiated.");

        TimerView timerViewComponent = timerGameObject.GetComponentInChildren<TimerView>();
        if (timerViewComponent != null)
        {
            TextLog.Instance.Log("TimerViewModel: Setting timer.");
            timerViewComponent.SetTimer(newTimeValue, stepId);
            TextLog.Instance.Log("Timer started for step ID: " + stepId);
        }
        else
        {
            TextLog.Instance.Log("Failed to get TimerView component from instantiated timer prefab.");
            return;
        }

        TimerModel newTimer = new TimerModel(newTimeValue, true, stepId, timerGameObject);
        timers.Add(newTimer);
        TextLog.Instance.Log("TimerViewModel: Timer added to list.");
    }



    // public void AddTimer(float newTimeValue, int associatedStepId)
    // {
    //     if (timerPrefab == null)
    //     {
    //         TextLog.Instance.Log("Timer prefab is not assigned.");
    //         return;
    //     }

    //     GameObject timerGameObject = Instantiate(timerPrefab, timerPanel);
    //     TextLog.Instance.Log("Timer instantiated.");
    //     TimerView timerViewComponent = timerGameObject.GetComponent<TimerView>();
    //     if (timerViewComponent != null)
    //     {
    //         timerViewComponent.SetTimer(newTimeValue, currentTimerId);
    //         TextLog.Instance.Log("Timer started with ID: " + currentTimerId);
    //     }
    //     else
    //     {
    //         TextLog.Instance.Log("TimerView component not found on the instantiated prefab.");
    //     }

    //     TimerModel newTimer = new TimerModel(newTimeValue, true, currentTimerId, timerGameObject, associatedStepId);
    //     timers.Add(newTimer);
    //     currentTimerId++;
    // }


    private void CompleteTimer(TimerModel timer)
    {
        OnTimerCompleted?.Invoke(timer.timerId);
    }


    public void RemoveTimerById(string timerId)
    {
        var timerToRemove = timers.Find(timer => timer.timerId == timerId);
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