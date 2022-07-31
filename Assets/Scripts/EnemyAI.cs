using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float ChaseRange;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    public void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update(){
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    public void FixedUpdate(){
        Vector3 direction = player.position - transform.position;
        if (Math.Abs(direction.x) <= ChaseRange && Math.Abs(direction.y) <= ChaseRange){
            animator.SetBool("Move", true);
            moveCharacter();
        }
        else{
            animator.SetBool("Move", false);
        }
    }

    void moveCharacter(){
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.deltaTime));
    }


}
