using System;
using TMPro;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private TextMeshProUGUI debugText, numberText;
    private int numberOfCollidingFingers;
    // Start is called before the first frame update
    void Start()
    {
        debugText = GameObject.FindGameObjectWithTag("debug").GetComponent<TextMeshProUGUI>();
        numberText = GameObject.FindGameObjectWithTag("number").GetComponent<TextMeshProUGUI>();
        debugText.text = "Start";
    }
    
    private void Update()
    {
        UpdateNumberOfCollidingFingers();
    }
    
    private void UpdateNumberOfCollidingFingers()
    {
        numberText.text = "" + numberOfCollidingFingers;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsFinger(collision.gameObject))
        {
            numberOfCollidingFingers++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsFinger(collision.gameObject))
        {
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

   /* private void OnCollisionEnter(Collision other)
    {
        //Debug all the information about the collision
        Debug.Log("collision with " + other.gameObject.name + " at " + other.contacts[0].point);
        debugText.text = other.gameObject.name + " collided with " + gameObject.name + " at " + other.contacts[0].point;
    }
    
    //We update the number with the number of contacts
    private void OnCollisionStay(Collision collision)
    {
        numberOfCollidingFingers = 0; // Azzera il conteggio delle dita in collisione ad ogni frame

        // Itera su tutte le collisioni presenti nell'oggetto 'collision'
        foreach (ContactPoint contact in collision.contacts)
        {
            // Controlla se l'oggetto in collisione ha un tag che identifica le dita (ad esempio 'Finger')
            if (contact.otherCollider.CompareTag("finger"))
            {
                // Incrementa il conteggio delle dita in collisione
                numberOfCollidingFingers++;
            }
        }

        // Aggiorna il testo per mostrare il numero di dita in collisione con il tavolo
        numberText.text = "" + numberOfCollidingFingers;

    }*/
}
