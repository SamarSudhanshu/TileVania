using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed = 1.0f;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Move();
        Flip();   
    }
    private void Move()
    {
        rb.velocity = new Vector2 (speed , rb.velocity.y);
    }
    private void Flip()
    {
        if(!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        speed = -speed;
        transform.localScale = new Vector2 (Mathf.Sign(speed), 1f);
    }
}
