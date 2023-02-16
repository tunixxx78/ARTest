using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    public GameObject thisCar;
    public Vector3 movement;
    public Rigidbody RBInstance;

    bool move;
    [SerializeField] bool turning;
    
    void Awake()
    {
        if (buttonClick == null) { buttonClick = new UnityEvent(); }
        thisCar = GameObject.Find("Car");
        move = false;

    }

    private void Start()
    {
        RBInstance = thisCar.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (move && turning == false)
        {
            RBInstance.drag = 0;
            //RBInstance.velocity = movement;
            thisCar.transform.Translate(movement * Time.deltaTime,Space.Self);
        }

    }

    private void FixedUpdate()
    {
        if (turning && move)
        {
            RBInstance.MoveRotation(RBInstance.rotation * Quaternion.Euler(movement));
            Debug.Log("SISÄLLÄ KÄÄNTYMISESSÄ.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        move = true;
        print("DOWN!!!");
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        move = false;
        RBInstance.drag = 10;
        print("UP!!!");
    }
}
