using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // public enum AttackDirection{
    //     left, right
    // }

    // public AttackDirection attackDirection;
    public float damage = 3;
    private Vector2 rightAttackOffset;
    public Collider2D swordCollider;

    private void Start(){
        // swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }

    // public void Attack(){
    //     switch(attackDirection){
    //         case AttackDirection.left:
    //             AttackLeft();
    //             break;
    //         case AttackDirection.right:
    //             AttackRight();
    //             break;
    //     }
    // }

    public void AttackRight() {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null){
                enemy.Health -= damage;
            }
        }
    }
}
