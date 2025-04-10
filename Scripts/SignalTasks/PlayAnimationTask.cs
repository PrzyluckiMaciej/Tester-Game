using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationTask : SignalTask
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private string animatorOption;

    public override void Execute() {
        animator.Play(animatorOption);
    }
}
