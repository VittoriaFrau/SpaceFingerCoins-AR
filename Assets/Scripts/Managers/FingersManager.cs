using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using UI;
using UnityEngine;

public class FingersManager : MonoBehaviour
{
    private List<GameObject> fingerNumbers = new();
    //I use those variables to check if the finger numbers are already shown
    private bool showFingerNumbersLeft;
    private bool showFingerNumbersRight;
    public List<GameObject> fingerNumbersLeft = new ();
    public List<GameObject> fingerNumbersRight = new();
    
    private Dictionary<int, Vector3> fingerNumbersLocalPositions = new();
    private Dictionary<int, Vector3> fingerNUmbersLocalRotations = new();
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(fingerNumbersLeft.Count == 0 || fingerNumbersRight.Count == 0)
            FindFingerNumbers();
        // Finger numbers are divided into left and right hand
        fingerNumbers = fingerNumbersLeft.Concat(fingerNumbersRight).ToList();
        HideAllFingerNumbers();

        //Check if the finger lists are complete
        if (fingerNumbers.Count != 10)
            Debug.LogError("Finger numbers are not complete");
        if(fingerNumbersLeft.Count != 5)
            Debug.LogError("Finger numbers left are not complete");
        if(fingerNumbersRight.Count != 5)
            Debug.LogError("Finger numbers right are not complete");
        
        //Save the initial position of the finger numbers
        SaveInitialData();
    }
    
    private void SaveInitialData()
    {
        for (int i = 0; i < fingerNumbers.Count; i++)
        {
            fingerNumbersLocalPositions.Add(i, fingerNumbers[i].transform.localPosition);
            fingerNUmbersLocalRotations.Add(i, fingerNumbers[i].transform.localEulerAngles);
        }
       
    }
    
    private void FindFingerNumbers()
    {
        fingerNumbers = GameObject.FindGameObjectsWithTag("FingerNumber").ToList();
        fingerNumbersLeft = fingerNumbers.Where(f => f.name.Contains("Left")).ToList();
        fingerNumbersRight = fingerNumbers.Where(f => f.name.Contains("Right")).ToList();
    }
    
    public void ShowFingerNumbers(bool isLeftHand)
    {
        if (isLeftHand && !showFingerNumbersLeft)
        {
            ShowFingers(fingerNumbersLeft);
            showFingerNumbersLeft = true;
        }
        else if(!showFingerNumbersRight)
        {
            ShowFingers(fingerNumbersRight);
            showFingerNumbersRight = true;
        }
    }
    
    private void ShowFingers(List<GameObject> fingers){
        foreach (var finger in fingers)
        {
            finger.SetActive(true);
        }
    }

    private void ShowAllFingerNumbers()
    {
        foreach (var fingerNumber in fingerNumbers)
        {
            fingerNumber.SetActive(true);
        }
    
    }
    
    public void HideAllFingerNumbers()
    { 
        foreach (var fingerNumber in fingerNumbers)
        {
            fingerNumber.SetActive(false);
        }
    }
    
    public void HideFingerNumbers(bool isLeftHand)
    {
        if (isLeftHand && showFingerNumbersLeft)
        {
            HideFingers(fingerNumbersLeft);
            showFingerNumbersLeft = false;
        }
        else if(showFingerNumbersRight)
        {
            HideFingers(fingerNumbersRight);
            showFingerNumbersRight = false;
        }
    }
    
    private void HideFingers(List<GameObject> fingers){
        foreach (var finger in fingers)
        {
            finger.SetActive(false);
        }
    }
    
    public void CreateCopyOfObject(GameObject obj)
    {
        //Vector3 initialPosition = obj.transform.position;
        GameObject newObj = Instantiate(obj);
        //block the grabbable to avoid the object to be grabbed immediately
        obj.GetComponent<Grabbable>().enabled = false;
        newObj.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);
        newObj.transform.localScale = obj.transform.localScale;
        newObj.name = obj.name + "Copy";
        Destroy(newObj.GetComponent<InteractableUnityEventWrapper>());
        newObj.transform.parent = null;

        
        Vector3 localPosition = GetPositionFromFingerNumber(obj);
        Vector3 localRotation = GetRotationFromFingerNumber(obj);
        
        //TODO capire quale di questi due per togliere la velocita funziona
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        obj.transform.localPosition = localPosition;
        obj.transform.localEulerAngles = localRotation;
        
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        obj.GetComponent<Grabbable>().enabled = true;
    }
    
    private IEnumerator EnableGrabbable(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.GetComponent<Grabbable>().enabled = true;
    }

    private Vector3 GetPositionFromFingerNumber(GameObject finger)
    {
        //Find the index of the finger number
        int index = fingerNumbers.IndexOf(finger);
        //Get the position of the finger number
        return fingerNumbersLocalPositions[index];
    }
    
    private Vector3 GetRotationFromFingerNumber(GameObject finger)
    {
        //Find the index of the finger number
        int index = fingerNumbers.IndexOf(finger);
        //Get the position of the finger number
        return fingerNUmbersLocalRotations[index];
    }

    
    
}
