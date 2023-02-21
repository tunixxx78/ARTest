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
        aRAim = FindObjectOfType<ARAimPoint>();
        isBoosted = false;
    }

    private void Start()
    {
        originalSpeed = aRAim.carSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isMercury && isBoosted == false)
        {
            isBoosted = true;
            aRAim.carSpeed = boostedSpeed;

            Debug.Log("Speed is boosted and current speed is: " + aRAim.carSpeed);

            //Destroy(this.gameObject, 1f);

            StartCoroutine(TurnSpeedToDefault());
        }
        if(other.CompareTag("Player") && isNeptun)
        {
            aRAim.timeToGoIndex += extraTime;

            Destroy(this.gameObject, 2f);
        }
    }

    IEnumerator TurnSpeedToDefault()
    {
        yield return new WaitForSeconds(boostTime);
        aRAim.carSpeed = originalSpeed;
        isBoosted = false;

    }
}
