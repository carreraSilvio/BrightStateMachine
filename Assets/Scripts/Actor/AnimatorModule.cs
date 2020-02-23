using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorModule : ActorModule
{
    [SerializeField] private Animator _animator;

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }


}
