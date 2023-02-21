using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLRBoost : MonoBehaviour
{
    [SerializeField] float originalSpeed, boostedSpeed;
    [SerializeField] float extraTime, boostTime;

    [SerializeField] bool isMercury, isNeptun, isBoosted = false;

    ARAimPoint aRAim;

    private void Awake()
    {
        originalSpeed = aRAim.carSpeed;
        isBoosted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isMercury && isBoosted == false)
        {
            isBoosted = true;
            aRAim.carSpeed = boostedSpeed;

            Destroy(this.gameObject, 1f);

            StartCoroutine(TurnSpeedToDefault());
        }
        if(other.CompareTag("Player") && isNeptun)
        {
            aRAim.timeToGoIndex += extraTime;
        }
    }

    IEnumerator TurnSpeedToDefault()
    {
        yield return new WaitForSeconds(boostTime);
        aRAim.carSpeed = originalSpeed;
        isBoosted = false;

    }
}
