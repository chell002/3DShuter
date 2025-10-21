using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public GameObject gameObjectEnemy;
    public Transform transformEnemy;
    private void OnTriggerEnter(Collider other)
    {
        gameObjectEnemy.SetActive(true);
        transformEnemy.localScale = new Vector3(3,3,3);
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        transformEnemy.localScale = new Vector3(1,1,1);
    }
}
