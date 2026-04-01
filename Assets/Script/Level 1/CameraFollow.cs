using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity= Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition=player.position+offset;
        desiredPosition.z=transform.position.z;
       this.transform.position= Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
