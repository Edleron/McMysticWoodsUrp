using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.SendMessage("OnHit", swordDamage);    
    }
}
