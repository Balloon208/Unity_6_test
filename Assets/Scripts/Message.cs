using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] Text messageText;
    
    public void SetText(string str)
    {  
        messageText.text = str; 
    }
}
