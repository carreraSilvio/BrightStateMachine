using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimCommand : Command
{
    public string stateName;

    public override void Execute(Actor actor)
    {
        var anim = actor.animator.GetComponent<Animator>();
        anim.Play(stateName);
    }
}
