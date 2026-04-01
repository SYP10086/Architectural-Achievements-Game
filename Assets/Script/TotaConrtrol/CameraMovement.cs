using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{

    public float Speed = 10f;
    private Vector3 targetPosition;
    public float smoothTime = 0.1f;
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    Vector3 velocity= Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        SpriteRenderer bg=GameObject.Find("Background")?.GetComponent<SpriteRenderer>();
        if (bg != null)
        {
            Bounds bounds = bg.bounds;
             minX=bounds.min.x+10;
             //minY=bounds.min.y+5;
             maxX=bounds.max.x-10;
             //maxY=bounds.max.y-5;
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        float horizontal = Input.GetAxis("Horizontal");
       // float vertical = Input.GetAxis("Vertical");
        moveDir = new Vector3(horizontal, 0, 0).normalized;
        

        if (moveDir != Vector3.zero)
        {
            targetPosition += Speed * moveDir*Time.deltaTime;
         
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y=Mathf.Clamp(targetPosition.y, minY, maxY);
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity, smoothTime);
        

    }
}
