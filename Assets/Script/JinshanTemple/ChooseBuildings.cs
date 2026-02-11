using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseBuildings : MonoBehaviour
{
    [SerializeField] public float boxX, boxY;
    private bool isDragging=false;
    private bool hasExecuted = false;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private int num;
    private Rigidbody rb;
    private Vector3 currentVelocity;
    private float smoothForwardTime = 0.3f;
    private float smoothBackTime = 0.5f;
    private float maxSpeed = Mathf.Infinity;
    private float zPosition = 0f;
    private float distance ;
    private float r = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isDragging = false;
        hasExecuted = false;
}
    
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, destination);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isDragging)
        {
            mousePosition.z = zPosition;
            transform.position = mousePosition;
        }

        if (!isDragging && distance <= r&&num==DestinationMovement.DestinationCount)
        {
            transform.position = Vector3.SmoothDamp(
            transform.position,
            destination,
            ref currentVelocity,
            smoothForwardTime,
            maxSpeed
            );
            
        }
        else if(!isDragging && distance >= r&&transform.position!=originalPosition)
        {
            transform.position = Vector3.SmoothDamp(
            transform.position,
            originalPosition,
            ref currentVelocity,
            smoothBackTime,
            maxSpeed
            );
        }

        if (transform.position == destination&& !hasExecuted)
        {
            //特效
            hasExecuted = true;
            DestinationMovement.DestinationCount++;
        }
        Debug.Log(DestinationMovement.DestinationCount);
    }

    private void OnMouseDown()
    {
        isDragging=true;
    }

    private void OnMouseUp()
    {
        currentVelocity = rb.velocity;
        distance =Vector3.Distance( transform.position,destination);
        isDragging = false;
        
    }

    private void OnMouseOver()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position == originalPosition)
        {
            if (mousePosition.x <= originalPosition.x + boxX / 2 && mousePosition.x >= originalPosition.x - boxX / 2
            && mousePosition.y <= originalPosition.y + boxY / 2 && mousePosition.y >= originalPosition.y - boxY / 2)
            {
                Debug.Log("显示资料");


            }
        }
        
    }
}
