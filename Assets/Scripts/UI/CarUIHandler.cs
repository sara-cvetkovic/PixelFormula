using UnityEngine;

public class CarUIHandler : MonoBehaviour
{

    //Other components
    Animator animator = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartCarEntranceAnimation(bool isAppearingOnRightSide)
    {
        if (isAppearingOnRightSide)
            animator.Play("Car UI Appear From Right");
        else animator.Play("Car UI Appear From Left");
    }

    public void StartCarExitAnimation(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
            animator.Play("Car UI Disappear From Right");
        else animator.Play("Car UI Disappear From Left");
    }

    //Events
    public void OnCarExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}
