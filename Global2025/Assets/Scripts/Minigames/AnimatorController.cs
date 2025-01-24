using UnityEngine;

public class AnimatorController : GameComponent
{
    private Animator _animator;
    
    void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.enabled = true;
    }

    private void OnDisable()
    {
        _animator.enabled = false;
    }
}
