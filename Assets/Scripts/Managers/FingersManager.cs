using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FingersManager : MonoBehaviour
{
    private List<GameObject> fingerNumbers;
    private bool showFingerNumbersLeft = false;
    private bool showFingerNumbersRight = false;
    private bool showFingerNumbers = false;
    public List<GameObject> fingerNumbersLeft;
    public List<GameObject> fingerNumbersRight;
    
    // Start is called before the first frame update
    void Start()
    {
        HideAllFingerNumbers();
        if(fingerNumbersLeft.Count == 0 || fingerNumbersRight.Count == 0)
            FindFingerNumbers();
        // Finger numbers are divided into left and right hand
        fingerNumbers = fingerNumbersLeft.Concat(fingerNumbersRight).ToList();
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
        if(!showFingerNumbers)
        {
            foreach (var fingerNumber in fingerNumbers)
            {
                fingerNumber.SetActive(true);
            }
            showFingerNumbers = true;
        }
    }
    
    public void HideAllFingerNumbers()
    {
        if(showFingerNumbers)
        {
            foreach (var fingerNumber in fingerNumbers)
            {
                fingerNumber.SetActive(false);
            }
            showFingerNumbers = false;
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
}
