using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DestinationMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] destination=new Vector3[4];
    public static int DestinationCount { get; set; } =0;
    // Start is called before the first frame update
    void Start()
    {
        DestinationCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (DestinationCount){
            case 0:
                transform.position = destination[0];
                break;
            case 1:
                transform.position = destination[1];
                break;
            case 2:
                transform.position = destination[2];
                break;
            case 3:
                transform.position = destination[3];
                break;
        }
    }
}
