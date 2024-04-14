using UnityEngine;

public class TransitionController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PopIn()
    {
        animator.Play("PopUpAnimation");
    }

    public void PopOut()
    {
        animator.Play("PopOutAnimation");
    }
}
