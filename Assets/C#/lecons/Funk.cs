using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funk : MonoBehaviour
{
    public int HP = 1;
    public int maxHp = 100;
    public int damege = 50;
    public int hill = 50;
    

    private void Awake()
    {
        HP = maxHp;
    }
    private void OnEnable()
    {
        HP -= damege;
    }
    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        
    }
    private void Start()
    {
        Hill(hill);
    }
    private void Update()
    {

    }
    private void LateUpdate()
    {
        
    }
    private void FixedUpdate() 
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private void OnMouseEnter()
    {
        
    }
    private void OnMouseDrag()
    {
        
    }
    private void OnMouseExit()
    {
        
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        
    }
    public void Hill(int hill )
    {
        HP += hill;
        print(HP);
    }
}
