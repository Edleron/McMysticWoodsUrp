using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron;

public class DamagableCharacters : MonoBehaviour, IDamagable
{
    private Animator animator;
    private Collider2D col;
    private Rigidbody2D rb;

    public float _health            = 3; 
    private bool isAlive            = true;
    public bool  _targetable        = true;
    public bool disableSimulation   = false;

    public float Healt {
        get {
            return _health;
        }
        set {
            if (value < _health) animator.SetTrigger("hit");

            _health = value;            

            if (_health <= 0)
            {
                // Destroy(gameObject);
                isAlive = false;
                Targetable = false;
                animator.SetBool("isAlive", isAlive);
            }
        }
    }

    public bool Targetable { get { return _targetable;} set { 
        _targetable = value; 
        if (disableSimulation) rb.simulated = false; 
        col.enabled = false; 
        } 
    }

    private void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        col      = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
    }

    public void OnHit(float damage)
    {
        Debug.Log("Silme Hit For ! " + damage);
        Healt -= damage;
    }

    public void OnHit(float damage, Vector2 knobcback)
    {
        Healt -= damage;
        rb.AddForce(knobcback);

        Debug.Log(knobcback);
    }

    public void OnDestroySelf()
    {
        Destroy(gameObject, 2);
    }
}
