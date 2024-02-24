using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron;
using System.Runtime.InteropServices.WindowsRuntime;

public class SlimeController : MonoBehaviour
{
    private float damage         = 1;    
    private float knockbackForce = 400f;
    private float moveSpeed      = 500f;
    private bool slimeLock       = false;

    private Rigidbody2D rb;
    private DetectionZone dz;
    private DamagableCharacters damagableCharacters;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        dz = GetComponentInChildren<DetectionZone>();
        damagableCharacters = GetComponent<DamagableCharacters>();
    }

    private void FixedUpdate() {
        if (slimeLock)
        {
            return;
        }

        if (damagableCharacters.Targetable && dz.detectedObjs.Count > 0)
        {
            Collider2D player = dz.detectedObjs[0];
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning(other.gameObject.name);

            slimeLock = true;

            Collider2D colllider = other.collider;
            IDamagable damagable = colllider.GetComponent<IDamagable>();

            if (damagable != null)
            {
                // Vector3 parentPosition = transform.parent.position;
                Vector2 direction      = (colllider.transform.position - transform.position).normalized;
                Vector2 knokcback      = direction * knockbackForce;

                // other.SendMessage("OnHit", swordDamage, knocback);   
                damagable.OnHit(damage, knokcback);

                // Debug.Log("Slider Damagable");
            }
            else 
            {
                Debug.LogWarning("NOT Damagable");
            }       
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.LogWarning(other.gameObject.name);

            slimeLock = false;
        }
    }
}
