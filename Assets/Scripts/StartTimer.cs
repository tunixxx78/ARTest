using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    ARAimPoint aRAim;

    private void Awake()
    {
        aRAim = FindObjectOfType<ARAimPoint>();
    }

    public void StartGameTimer()
    {
        aRAim.timerIsOn = true;
    }
}
