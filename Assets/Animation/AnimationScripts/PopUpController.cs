using UnityEngine;

public class PopUpController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerPopUp()
    {
        animator.Play("PopUpAnimation");
    }
}
