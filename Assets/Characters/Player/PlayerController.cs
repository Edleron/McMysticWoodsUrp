using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // Each frame of physics, what percentage of the speed should be shaved off the velocity out of 1 (100%)
    private bool canMove      = true;
    private bool isMoving     = false;
    private float maxSpeed     = 5f;
    private float moveSpeed    = 750f;
    private float idleFriction = 0.9f;
    private Vector2 moveInput = Vector2.zero;

    private Rigidbody2D rb;
    private Animator animator;
    public GameObject swordHitBox;
    private Collider2D swordCollider;
    private SpriteRenderer spriteRenderer;

    private bool IsMoving { 
        set { 
            isMoving = value; 
            animator.SetBool("isMoving", isMoving);
        }
    }

    void Start(){
        rb              = GetComponent<Rigidbody2D>();
        animator        = GetComponent<Animator>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        swordCollider   = swordHitBox.GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        
        if(canMove && moveInput != Vector2.zero) {
            // Move animation and add velocity

            // Accelerate the player while run direction is pressed
            // BUT don't allow player to run faster than the max speed in any direction
            // rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);

            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed , idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            // Control whether looking left or right
            if(moveInput.x > 0) {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacing", true);
            } else if (moveInput.x < 0) {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacing", false);
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
