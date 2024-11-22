using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    // Animator Hashes
    private int isWalkingHash = Animator.StringToHash("isWalking");
    private int isJumpingHash = Animator.StringToHash("isJumping");

    // Ground Check
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer;           // Assign this in the Inspector to match the ground's layer

    void Start()
    {
        // Get Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleWalking();
        HandleJumping();
    }

    private void HandleWalking()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

    private void HandleJumping()
    {
        bool isGrounded = IsGrounded();
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (isGrounded && jumpPressed)
        {
            // Trigger the jump animation
            animator.SetBool(isJumpingHash, true);

            // Optional: Add a short delay to reset the jump animation
            StartCoroutine(ResetJumpAnimation());
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast down to check for the ground
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private IEnumerator ResetJumpAnimation()
    {
        // Wait for a fraction of a second to let the animation play
        yield return new WaitForSeconds(0.1f);

        // Reset the jump animation
        animator.SetBool(isJumpingHash, false);
    }
}