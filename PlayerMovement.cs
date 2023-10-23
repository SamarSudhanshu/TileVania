using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float jumpAmount = 10.0f;
    [SerializeField] private float climbSpeed = 10.0f;
    private float gravityScaleAtStart;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider2D;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private bool isAlive = true;
    private Color deathColor = new Color (1f, 0.5f, 0.5f);
    SpriteRenderer spriteRenderer;
    private Color objectColor;
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletGameObject;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if(!isAlive)
            return;
        Run();
        Flip();
        ClimbLadder();
        Die();
    }
    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    private void Run() {
        Vector2 playerInput = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerInput;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }
    private void Flip() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    private void OnJump(InputValue value) {
        if(!isAlive)
            return;
        if(!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        rb.velocity += new Vector2(0f, jumpAmount);
    }

    private void ClimbLadder() {
        if(!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("IsClimbingLadder", false);
            return;
        }
        rb.gravityScale = 0.0f;
        Vector2 playerInput = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = playerInput;
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("IsClimbingLadder", playerHasVerticalSpeed);
    }

    private void Die()
    {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
            isAlive = false;
            animator.SetTrigger("IsDying");
            rb.velocity = new Vector2(0f, 24f);
            spriteRenderer.color = deathColor;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
            isAlive = false;
            animator.SetTrigger("IsDying");
            rb.velocity = new Vector2(0f, 24f);
            spriteRenderer.color = deathColor;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy"))) {
            isAlive = false;
            animator.SetTrigger("IsDying");
            rb.velocity = new Vector2(0f, 24f);
            spriteRenderer.color = deathColor;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void OnFire(InputValue value) {
        if(!isAlive)
            return;
        Instantiate(bulletGameObject, gun.position, gun.rotation);
    }
}
