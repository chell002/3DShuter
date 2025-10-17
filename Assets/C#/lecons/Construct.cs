using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Construct : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public bool isShooText;
    public string nuwText;
    public int num;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (isShooText && num > 2 || num == 2)
        {
            textMeshProUGUI.text = nuwText; 
        }
        else
        {
            textMeshProUGUI.text = "No";
        }
    }
}
