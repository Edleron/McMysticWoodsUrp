using System;
using System.Collections;
using System.Collections.Generic;
using Edleron;
using Unity.VisualScripting;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 1.0f;
    public float knockbackForce = 7500f;
    public Collider2D swordCollider;    
    public Vector3 faceRight = new Vector3(1, -0.9f, 0);
    public Vector3 faceLeft  = new Vector3(-1, -0.9f, 0);

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
        
        IDamagable damagableObject = other.GetComponent<IDamagable>();

        if (damagableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction      = (Vector2) (other.gameObject.transform.position - parentPosition).normalized;
            Vector2 knokcback      = direction * knockbackForce;

            // other.SendMessage("OnHit", swordDamage, knocback);   
            damagableObject.OnHit(swordDamage, knokcback);
        } 
        else 
        {
            Debug.LogWarning("NOT Damagable");
        }       
    }

    private void IsFacing(bool isFacing) {
       gameObject.transform.localPosition = isFacing ? faceRight : faceLeft;
    }
}
