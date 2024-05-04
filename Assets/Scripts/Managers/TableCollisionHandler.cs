using System;
using TMPro;
using UI;
using UnityEngine;

public class TableCollisionHandler : MonoBehaviour
{

    private int numberOfCollidingFingers;
    private bool countFingers = true;
    private bool isColliding = false;
    private float collisionStartTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsFinger(collision.gameObject) && countFingers)
        {
            isColliding = true;
            numberOfCollidingFingers++;
        }else isColliding = false;
    }

    private void OnCollisionStay(Collision other)
    {
        //if the collision stay happens for at least 1.5 seconds
        if (isColliding && countFingers)
        {
            DebugTextManager.Instance.SetDebugText("Collision stay, fingers: " + numberOfCollidingFingers);
            // Check if collision has been continuously happening for at least 1.5 seconds
            if (Time.time - collisionStartTime >= 1.5f)
            {
                TilesManager.Instance.SetActiveTileText("" + numberOfCollidingFingers); 
            }
        }

    }

    
    private void OnCollisionExit(Collision collision)
    {
        if (IsFinger(collision.gameObject))
        {
            isColliding = false;
            numberOfCollidingFingers--;
            if (numberOfCollidingFingers < 0)
            {
                numberOfCollidingFingers = 0;
            }
        }
    }

    private bool IsFinger(GameObject obj)
    {
        // Controlla se l'oggetto ha un tag che identifica le dita (ad esempio 'Finger')
        return obj.CompareTag("finger");
    }

    public void StopCountingFingers()
    {
        countFingers = false;
    } 
   
}
