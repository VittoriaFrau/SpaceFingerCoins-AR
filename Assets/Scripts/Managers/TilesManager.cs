using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    
    public static TilesManager Instance { get; private set; }
    private GameObject activeTile;
    private TextMeshProUGUI activeTileText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate instance of TilesManager. Destroying...");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        activeTile = GameObject.FindGameObjectWithTag("NumberTile");
        activeTileText = activeTile.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetCurrentActiveTile(GameObject tile)
    {
        activeTile = tile;
        activeTileText = tile.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetActiveTileText(string text)
    {
        activeTileText.text = text;
        activeTile.GetComponent<NumberTile>().StartFadeOut();
    }
}
