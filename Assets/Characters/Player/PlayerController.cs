using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    public float idleFriction = 0.9f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput = Vector2.zero;

    private bool canMove = true;
    private bool isMoving = false;
    private bool IsMoving { 
        set { 
            isMoving = value; 
            animator.SetBool("isMoving", isMoving);
        }
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        
        if(canMove && moveInput != Vector2.zero) {
            // Move animation and add velocity

            // Accelerate the player while run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);

            // Control whether looking left or right
            if(moveInput.x > 0) {
                spriteRenderer.flipX = false;
            } else if (moveInput.x < 0) {
                spriteRenderer.flipX = true;
            }          

            IsMoving = true;  
        } else {
            // No movement so interpolate velocity towards 0
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }
    }


    // Get input values for player movement
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    // Animator Events
    void LockMovement() {
        canMove = false;
    }

    // Animator Events
    void UnLockMovement(){
        canMove = true;
    }
}
