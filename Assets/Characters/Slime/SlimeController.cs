using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float Healt {
        set {
            _health = value;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get {
            return _health;
        }
    }

    public float _health = 3;
    private void OnHit(float damage)
    {
        Debug.Log("Silme Hit For ! " + damage);
        Healt -= damage;
    }
}
