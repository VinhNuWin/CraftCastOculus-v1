using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public void TriggerAllPopOuts()
    {
        foreach (var controller in FindObjectsOfType<TransitionController>())
        {
            controller.PopOut();
        }
    }

    public void TriggerAllPopIns()
    {
        foreach (var controller in FindObjectsOfType<TransitionController>())
        {
            controller.PopIn();
        }
    }
}
