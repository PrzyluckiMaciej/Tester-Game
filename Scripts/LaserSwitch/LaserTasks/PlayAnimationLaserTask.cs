using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationLaserTask : LaserTask {
    [SerializeField] private Animator animator = null;
    [SerializeField] private string animatorOption;

    public override void Execute() {
        animator.Play(animatorOption);
    }
}
