using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody carRB;
    ARAimPoint aRAim;

    private void Awake()
    {
        carRB = GetComponent<Rigidbody>();
        aRAim = FindObjectOfType<ARAimPoint>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            aRAim.losePanel.SetActive(true);
            aRAim.plrControllers.SetActive(false);
            Destroy(gameObject, 1f);

        }
    }


}
