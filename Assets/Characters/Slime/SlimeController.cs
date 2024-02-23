using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron;
using System.Runtime.InteropServices.WindowsRuntime;

public class SlimeController : MonoBehaviour
{
    private float damage        = 1;    

    private void OnCollisionEnter2D(Collision2D other) {
        IDamagable damagable = other.collider.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.OnHit(damage);
        }
    }
}
