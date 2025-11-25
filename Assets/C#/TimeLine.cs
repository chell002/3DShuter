using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLine : MonoBehaviour
{
    public PlayableDirector Dir;
    public GameObject VirtCams;
    private void OnEnable()
    {
        Dir.stopped += Dir_stopped;
    }

    private void Dir_stopped(PlayableDirector obj)
    {
        VirtCams.SetActive(false);
    }

 
    
}
