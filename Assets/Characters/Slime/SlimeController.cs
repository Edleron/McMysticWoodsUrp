using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Animator animator;
    private bool isAlive = true;

    public float Healt {
        set {
            if (value < _health)
            {
                animator.SetTrigger("hit");
            }

            _health = value;            

            if (_health <= 0)
            {
                // Destroy(gameObject);
                isAlive = false;
                animator.SetBool("isAlive", isAlive);
            }
        }
        get {
            return _health;
        }
    }

    public float _health = 3;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
    }

    private void OnHit(float damage)
    {
        Debug.Log("Silme Hit For ! " + damage);
        Healt -= damage;
    }
}
