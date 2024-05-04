using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FingersManager : MonoBehaviour
{
    private List<GameObject> fingerNumbers = new();
    //I use those variables to check if the finger numbers are already shown
    private bool showFingerNumbersLeft;
    private bool showFingerNumbersRight;
    public List<GameObject> fingerNumbersLeft = new ();
    public List<GameObject> fingerNumbersRight = new();
    
    // Start is called before the first frame update
    void Start()
    {
        if(fingerNumbersLeft.Count == 0 || fingerNumbersRight.Count == 0)
            FindFingerNumbers();
        // Finger numbers are divided into left and right hand
        fingerNumbers = fingerNumbersLeft.Concat(fingerNumbersRight).ToList();
        HideAllFingerNumbers();

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
        GameObject newObj = Instantiate(obj);
        newObj.transform.SetParent(obj.transform.parent);
        newObj.transform.SetLocalPositionAndRotation(obj.transform.localPosition, obj.transform.localRotation);
        newObj.transform.localScale = obj.transform.localScale;
        newObj.name = obj.name + "Copy";
        obj.transform.SetParent(null);
    }
    
    
}
