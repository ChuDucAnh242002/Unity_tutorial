using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    Animator animator;

    // Like a function
    public float Health {
        get {
            return health;
        }
        set {
            health = value;

            if (health <= 0){
                Defeated();
            }
            
            animator.SetTrigger("Damaged");
        }
    }
    
    public void Start(){
        animator = GetComponent<Animator>();
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }
}
