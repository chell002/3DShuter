using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnim : MonoBehaviour
{
    private Animator anim;
    private float switchAngleTurn = 45f;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Aim(bool isAim)
    {
        anim.SetBool("AimBool", isAim);
    }
    public void JampAnim()
    {
        anim.SetTrigger("JampTrigger");
    }
    
    public void PlayPersonAnim(Vector3 m_Input, bool isRunning)
    {
        float animationSpeed = isRunning ? 1 : 0.5f;
        if (m_Input.sqrMagnitude > 0)
        {
            anim.SetFloat("X", m_Input.x * animationSpeed, 0.1f, Time.deltaTime);
            anim.SetFloat("Z", m_Input.z * animationSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("X", 0, 0.1f, Time.deltaTime);
            anim.SetFloat("Z", 0, 0.1f, Time.deltaTime);
        }
    }
}
