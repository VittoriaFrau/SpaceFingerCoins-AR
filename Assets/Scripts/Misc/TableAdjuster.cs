using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class TableAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    private HandGrabInteractable handGrabInteractable;

    private void Start()
    {
        handGrabInteractable = GetComponent<HandGrabInteractable>();
    }

    public void MoveTable()
    {
        handGrabInteractable.enabled = !handGrabInteractable.enabled;
        Debug.Log("New table local position: " + transform.localPosition);
    }
}
