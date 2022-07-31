using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private BoxCollider2D hitbox;
    [SerializeField] private Animator animator;

    private void Start(){
        hitbox = GetComponent<BoxCollider2D>();

    }

    private void Update(){
        transform.localPosition = transform.position;
    }

    public bool OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        if(other.tag == "Enemy"){
            animator.SetTrigger("Die");
            return true;
        }    
        return false;
    }
}
