using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DestinationMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] destination=new Vector3[5];
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
            case 4:
                transform.position = destination[4];
                break;
        }
        if (DestinationCount == 5)
        {
            gameObject.SetActive(false);
            /*AchieveDetect.achieve[2] = true;
            PlayerPrefs.SetInt("achieve[2]", 1);*/
        }
    }
}
