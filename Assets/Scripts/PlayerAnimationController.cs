using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerJump()
    {
        SetLanded(false);
        animator.SetTrigger("JumpTrigger");
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetLanded(bool isLanded)
    {
        animator.SetBool("isLanded", isLanded);
    }

    public void SetFalling(bool isFalling)
    {
        animator.SetBool("isFalling", isFalling);
    }
}
