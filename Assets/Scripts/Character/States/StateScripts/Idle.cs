﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Hyukin's_Game/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl charControl = characterState.GetCharacterControl(animator);

        if (charControl.isJumping)
        {
            animator.SetBool("isJumping", true);
            return;
        }

        if (charControl.isMoving)
        {
            animator.SetBool("isRunning", true);
            return;
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
