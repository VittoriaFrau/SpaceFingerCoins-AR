using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrayCollision : MonoBehaviour
{
    private int numberOfCollisions = 0;
    public TextMeshProUGUI text;
    private float lastCollisionTime = -2f; 
    public float collisionCooldown = 2f;
    private const int ITEM_COST = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        float delta = Time.time - lastCollisionTime;
        Debug.Log("other.gameObject.name: " + other.gameObject.name);
        if (other.gameObject.name.Contains("index_finger"))
        {
            if (delta < collisionCooldown)
                return;
            numberOfCollisions++;
            text.text = "You paid: " + numberOfCollisions + "$\nYou need: "+ (ITEM_COST - numberOfCollisions) + "$" ;

        }
    }

}
