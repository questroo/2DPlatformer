using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorContoller : MonoBehaviour
{
    // Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_SPELLCAST = "Player_Spellcast";
    const string PLAYER_CROUCH = "Player_Crouch";
    const string PLAYER_DASH = "Player_Dash";
    const string PLAYER_JUMPATTACK = "Player_JumpAttack";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_BLOCK = "Player_Block";
    const string PLAYER_DIZZY = "Player_Dizzy";
    const string PLAYER_HURT = "Player_Hurt";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_STRIKE = "Player_Strike";
    const string PLAYER_CELEBRATION = "Player_Celebration";
    const string PLAYER_DIE = "Player_Die";

    Animator animator => GetComponent<Animator>();
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private string currentState;
    private int groundMask => 1 << LayerMask.NameToLayer("Ground");

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;
        animator.Play(newState);

        currentState = newState;
    }
}
