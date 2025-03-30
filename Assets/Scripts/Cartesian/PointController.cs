using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public TextMeshPro coordinatesText; // Reference to the UI text
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCoordinatesUI();
    }
    
    void UpdateCoordinatesUI()
    {
        if (coordinatesText != null)
        {
            float scaledX = Mathf.Abs(transform.position.x * 10) < 0.01f ? 0 : transform.position.x * 10;
            float scaledY = Mathf.Abs(transform.position.y * 10) < 0.01f ? 0 : transform.position.y * 10;
            float scaledZ = Mathf.Abs(transform.position.z * 10) < 0.01f ? 0 : transform.position.z * 10;
            coordinatesText.SetText($"<color=#FF0000>X: {scaledX:F1}</color>, <color=#00FF00>Y: {scaledY:F1}</color>, <color=#0000FF>Z: {scaledZ:F1}</color>");
        }
    }
}
