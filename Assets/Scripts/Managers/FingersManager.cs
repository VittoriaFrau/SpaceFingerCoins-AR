using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using TMPro;
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

    public GameObject applePrefab;
    public Transform tableTransform;
    public TextMeshProUGUI canvasText;
    public GameObject environmentParent;
    private Dictionary<int, Vector3> appleSpawningPositions = new();

    private int applesInTheScene = 0;
    
    private List<GameObject> fingerNumbersInTheScene = new(); // always not more than one
    
    // Start is called before the first frame update
    void Start()
    {
        if(fingerNumbersLeft.Count == 0) //|| fingerNumbersRight.Count == 0)
            FindFingerNumbers();
        // Finger numbers are divided into left and right hand
        fingerNumbers = fingerNumbersLeft.Concat(fingerNumbersRight).ToList();
        HideAllFingerNumbers();

        //Check if the finger lists are complete
       // if (fingerNumbers.Count != 10)
        //    Debug.LogError("Finger numbers are not complete");
        if(fingerNumbersLeft.Count != 5)
            Debug.LogError("Finger numbers left are not complete");
        //if(fingerNumbersRight.Count != 5)
         //   Debug.LogError("Finger numbers right are not complete");
        
        //Save the initial position of the finger numbers
        SaveInitialData();
        
        appleSpawningPositions.Add(1, new Vector3(-0.547999978f,0.777999997f,0.465999991f));
        appleSpawningPositions.Add(2, new Vector3(-0.268000007f,0.777999997f,0.230000004f));
        appleSpawningPositions.Add(3, new Vector3(0.0560000017f,0.777999997f,0.536000013f));
        appleSpawningPositions.Add(4, new Vector3(0.228f,0.777999997f,0.236000001f));
        appleSpawningPositions.Add(5, new Vector3(0.532000005f,0.777999997f,0.528999984f));
        appleSpawningPositions.Add(6, new Vector3(0.532000005f,0.777999997f,0.199000001f));
        appleSpawningPositions.Add(7, new Vector3(-0.194999993f,0.777999997f,0.572000027f));
        appleSpawningPositions.Add(8, new Vector3(-0.587000012f,0.777999997f,0.206f));
        appleSpawningPositions.Add(9, new Vector3(-0.0219999999f,0.777999997f,0.201000005f));
        appleSpawningPositions.Add(10, new Vector3(0.333999991f,0.777999997f,0.542999983f));
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
        if(fingerNumbersInTheScene.Count > 0)
        {
            foreach (var fingerNumber in fingerNumbersInTheScene)
            {
                Destroy(fingerNumber);
            }
        }
        
        //Vector3 initialPosition = obj.transform.position;
        GameObject newObj = Instantiate(obj);
        fingerNumbersInTheScene.Add(newObj);
        //block the grabbable to avoid the object to be grabbed immediately
        obj.GetComponent<Grabbable>().enabled = false;
        newObj.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);
        newObj.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
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

    public void SpawnApples(int digitNumber)
    {
        if(applesInTheScene != 0)
        {
            foreach (Transform child in environmentParent.transform)
            {
                if(child.gameObject.name.Contains("Apple"))
                    Destroy(child.gameObject);
            }
        }
        applesInTheScene = digitNumber;
        for (int i = 0; i < digitNumber; i++)
        {
            // Calcola la posizione con un offset per non sovrapporre le mele
            GameObject apple = Instantiate(applePrefab, environmentParent.transform);
            apple.transform.localPosition = appleSpawningPositions[i+1];
            apple.gameObject.SetActive(true);
        }
        canvasText.text = "Number: " + applesInTheScene;
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
