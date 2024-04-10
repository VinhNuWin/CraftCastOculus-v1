using System;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TimerModel
{
    public float timeValue;
    public bool timerActive;
    public int timerId;
    public GameObject timerGameObject;
    public bool removeListenerAdded;
    public int associatedStepId;
    public bool hasRun = false;

    public TimerModel(float timeValue, bool timerActive, int timerId, GameObject timerGameObject, int stepId)
    {
        this.timeValue = timeValue;
        this.timerActive = timerActive;
        this.timerId = timerId;
        this.timerGameObject = timerGameObject;
        this.removeListenerAdded = false;
        this.associatedStepId = associatedStepId;
    }
}