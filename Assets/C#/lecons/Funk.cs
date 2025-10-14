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
    private void Start()
    {
        Hill(hill);
    }
    private void Update()
    {

    }
    public void Hill(int hill )
    {
        HP += hill;
        print(HP);
    }
}
