using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    ARAimPoint aRAimPoint;

    private void Awake()
    {
        aRAimPoint = FindObjectOfType<ARAimPoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aRAimPoint.planetsToColectIndex--;
            Destroy(gameObject);
        }
    }
}
