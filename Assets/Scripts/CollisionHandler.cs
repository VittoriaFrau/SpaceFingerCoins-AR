using System;
using TMPro;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private TextMeshProUGUI debugText;
    private int numberOfCollidingFingers;

    public TextMeshProUGUI firstNumberText, secondNumberText;

    private int firstNumber, secondNumber;
    
    private bool isColliding = false;
    private float collisionStartTime = 0f;

    public GameObject tile; //TODO spawn the tile
    
    //TODO implement number selection (watch hierarchy UI)
    
    // Start is called before the first frame update
    void Start()
    {
        debugText = GameObject.FindGameObjectWithTag("debug").GetComponent<TextMeshProUGUI>();
        
        debugText.text = "Start";
    }
    
    /*private void Update()
    {
        if (numberOfCollidingFingers != 0)
        {
            UpdateNumberOfCollidingFingers();
        }
    }*/
    
    /*private void UpdateNumberOfCollidingFingers()
    {
        if (firstNumber == 0)
        {
            firstNumberText.text = "" + numberOfCollidingFingers;
            firstNumber = numberOfCollidingFingers;
        }
        else
        {
            firstNumber = secondNumber;
            secondNumber = numberOfCollidingFingers;
            firstNumberText.text = "" + firstNumber;
            secondNumberText.text = "" + secondNumber;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (IsFinger(collision.gameObject))
        {
            isColliding = true;
            numberOfCollidingFingers++;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //if the collision stay happens for at least 1.5 seconds
        if (isColliding)
        {
            // Check if collision has been continuously happening for at least 1.5 seconds
            if (Time.time - collisionStartTime >= 1.5f)
            {
                tile.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "" + numberOfCollidingFingers;
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

   
}
