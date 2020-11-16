using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorContoller : MonoBehaviour
{
    private bool isGrounded = true;
    private bool isAttacking = false;
    private bool isCrouching;
    private bool isJumpPressed;
    private bool isAttackPressed;
    private float xAxis;
    private string currentState;
    public float moveSpeed = 4.0f;
    public float jumpForce = 4.0f;
    public float attackDelay = 0.3f;
    public Transform footPosition;

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
    Rigidbody2D rb2d => GetComponent<Rigidbody2D>();
    private int groundMask;

    private void Start()
    {
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isAttackPressed = true;
        }

        isCrouching = Input.GetKey(KeyCode.DownArrow);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, 0.1f, groundMask);

        isGrounded = hit.collider != null ? true : false;

        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -moveSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = moveSpeed;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            vel.x = 0;
        }

        if(isGrounded && isCrouching)
        {
            ChangeAnimationState(PLAYER_CROUCH);
        }
        if (isGrounded && !isAttacking && !isCrouching)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if (isJumpPressed && isGrounded && !isCrouching)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        rb2d.velocity = vel;

        if (isAttackPressed)
        {
            isAttackPressed = false;
            if (!isAttacking)
            {
                isAttacking = true;
                if (isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    ChangeAnimationState(PLAYER_JUMPATTACK);
                }

                Invoke("AttackComplete", attackDelay);
            }
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;
        animator.Play(newState);

        currentState = newState;
    }
}
