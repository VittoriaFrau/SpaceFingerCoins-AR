using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileCollisions : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter, tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("NumberTile"))
        {
            Debug.Log("Collision with tile");
            //retrieve text of the tile
            text.text = collision.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
            Destroy(collision.gameObject);
            
        }
    }
}
