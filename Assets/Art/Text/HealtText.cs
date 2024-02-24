using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealtText : MonoBehaviour
{
    private float timeToLive = 0.5f;
    private float floatSpeed = 500.0f;
    private float timeElapsed = 0.0f;

    private Color startColor;
    private TextMeshProUGUI textMesh;
    private RectTransform rectTransform;
    private Vector3 floatDirection = new Vector3(0, 1, 0);

    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();

        startColor = textMesh.color;
    }

    private void Start()
    {
        
    }

  
    private void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;
        
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b , 1 - (timeElapsed / timeToLive));

        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
