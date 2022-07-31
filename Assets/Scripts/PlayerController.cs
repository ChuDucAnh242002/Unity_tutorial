using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float diagonalSpeed;
    [SerializeField] private float collisionOffset;
    [SerializeField] private ContactFilter2D movementFilter;
    [SerializeField] private SwordAttack swordAttack;
    
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate(){
        if(canMove){
            Move();
            FlipMove();
        }
    }

    private void TryMove(float speed){

        // return 0 and 1
        int count = rb.Cast(
            movementInput,
            movementFilter, 
            castCollisions, 
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        // Success move
        if(count == 0){
            // Param new position
            rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
            return;
        }
    }

    private void Move(){
        if(movementInput != Vector2.zero){
            if (movementInput.x != 0 && movementInput.y != 0){
                TryMove(diagonalSpeed);
            }
            else {
                TryMove(moveSpeed);
            }
            animator.SetBool("isMoving", true);
        }
        else{
            rb.velocity = new Vector2(0f, 0f);
            animator.SetBool("isMoving", false);
        }
    }

    private void FlipMove(){
        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        } 
        else if (movementInput.x > 0){
            spriteRenderer.flipX = false;
        }
    }

    // Press move
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    // Mouse button
    void OnAttack(){
        animator.SetTrigger("swordAttack");
        SwordAttack();
    }

    public void SwordAttack(){
        LockMovement();
        if (spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true; 
    }

    // public void OnCollisionEnter2D(Collider2D other){
    //     if(other.tag == "Enemy"){
    //         Health -= damage;
    //     }
    // }
}
