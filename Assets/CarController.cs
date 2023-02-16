using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody carRB;

    private void Awake()
    {
        carRB = GetComponent<Rigidbody>();
        
    }

    
}
