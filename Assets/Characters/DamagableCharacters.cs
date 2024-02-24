using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron;
using Unity.VisualScripting;

public class DamagableCharacters : MonoBehaviour, IDamagable
{
    public GameObject healtText;
    public bool disableSimulation           = false;
    public bool canTurnInvicible            = false;
    public float health                     = 3; 
    public bool  targetable                 = true;

    private bool _isAlive                    = true;  
    private bool _invincible                 = false;
    private float _invincibilityTime         = 5.0f;
    private float _invincibilityTimeElapsed  = 0.25f;

    private Animator _animator;
    private Collider2D _col;
    private Rigidbody2D _rb;

    public float Healt { get { return health; } set {
            if (value < health) 
            {
                _animator.SetTrigger("hit");
                RectTransform textTranform = Instantiate(healtText).GetComponent<RectTransform>();
                textTranform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTranform.SetParent(canvas.transform);
            }
           
            health = value;            

            if (health <= 0)
            {
                // Destroy(gameObject);
                _isAlive = false;
                Targetable = false;
                _animator.SetBool("isAlive", _isAlive);
            }
        }
    }

    public bool Targetable { get { return targetable;} set { 
        targetable = value; 
        if (disableSimulation) _rb.simulated = false; 
        _col.enabled = false; 
        } 
    }

    // BELİRLİ BİR SURE ZARAR GÖRMEME FEATURE !
    public bool Invincible { get { return _invincible; } set {
            _invincible = value;

            if (_invincible)
            {
                _invincibilityTimeElapsed = 0;
            }
        }
    }

    private void Start()
    {
        _rb       = GetComponent<Rigidbody2D>();
        _col      = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isAlive", _isAlive);
    }

    private void FixedUpdate() 
    {
        if (Invincible)
        {
            _invincibilityTimeElapsed += Time.deltaTime;

            if (_invincibilityTimeElapsed > _invincibilityTime)
            {
                Invincible = false;
            }
        }
    }


    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Healt -= damage;
             // Debug.Log("Silme Hit For ! " + damage);

            if (canTurnInvicible)
            {
                Invincible = true;
            }
        }
       
    }

    public void OnHit(float damage, Vector2 knobcback)
    {
        if (!Invincible)
        {
            Healt -= damage;
            _rb.AddForce(knobcback, ForceMode2D.Impulse);
            // Debug.Log("AddForce");

            if (canTurnInvicible)
            {
                Invincible = true;
            }
        }
    }

    public void OnDestroySelf()
    {
        Destroy(gameObject, 2);
    }
}
