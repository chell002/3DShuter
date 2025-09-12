using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int damage = 20;
    public int hp = 40;
    public int result;
    public string wepon = "ak47";

   public void Start()
    {
        result = hp - damage;
        Debug.Log("result: "+ result);
    }
   public void Update()
    {

    }

}