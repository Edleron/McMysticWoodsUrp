using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 1.0f;
    public Collider2D swordCollider;

    private void Start ()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider Not Set");
        }

        // swordCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.SendMessage("OnHit", swordDamage);    
    }
}
