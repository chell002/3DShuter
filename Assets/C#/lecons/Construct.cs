using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Construct : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public bool isShooText;
    public string nuwText;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isShooText)
        {
            textMeshProUGUI.text = nuwText; 
        }
        else
        {
            textMeshProUGUI.text = "No";
        }
    }
}
