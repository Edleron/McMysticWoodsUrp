using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private Collider2D col;
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    
   private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            detectedObjs.Add(other);
        }
   }

   private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            detectedObjs.Remove(other);
        }
   }
}
