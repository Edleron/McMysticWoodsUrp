using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron;
using System.Runtime.InteropServices.WindowsRuntime;

public class SlimeController : MonoBehaviour, IDamagable
{
    private Animator animator;
    private Collider2D col;
    private Rigidbody2D rb;

    public float Healt {
        get {
            return _health;
        }
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
                Targetable = false;
                animator.SetBool("isAlive", isAlive);
            }
        }
    }

    public bool Targetable { get { return _targetable;} set { _targetable = value; rb.simulated = value; col.enabled = false; } }

    public float _health = 3;
    public bool  _targetable = true;
    private bool isAlive = true;

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
}
