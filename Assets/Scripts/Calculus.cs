using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Calculus:MonoBehaviour
    {
        public TextMeshProUGUI firstNumber, secondNumber;
        public TextMeshProUGUI result, symbol;

        //TODO handle the case of changing the number 
        public void Sum()
        {
            int first = int.Parse(firstNumber.text);
            int second = int.Parse(secondNumber.text);
            result.text = "" + (first + second);
            symbol.text = "+";
        }
        
        public void Subtract()
        {
            int first = int.Parse(firstNumber.text);
            int second = int.Parse(secondNumber.text);
            result.text = "" + (first - second);
            symbol.text = "-";
        }
        
        public void Multiply()
        {
            int first = int.Parse(firstNumber.text);
            int second = int.Parse(secondNumber.text);
            result.text = "" + (first * second);
            symbol.text = "*";
        }
        
        public void Divide()
        {
            int first = int.Parse(firstNumber.text);
            int second = int.Parse(secondNumber.text);
            result.text = "" + (first / second);
            symbol.text = "/";
        }
        
        public void Clear()
        {
            firstNumber.text = "0";
            secondNumber.text = "0";
            result.text = "";
            symbol.text = "";
        }
    }
}