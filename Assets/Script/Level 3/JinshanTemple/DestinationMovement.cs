using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Profiling;

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
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        switch (DestinationCount){
            case 0:
                transform.position = destination[0];
                sr.sortingOrder = 1;
                break;
            case 1:
                transform.position = destination[1];
                sr.sortingOrder = 2;
                break;
            case 2:
                transform.position = destination[2];
                sr.sortingOrder = 2;
                break;
            case 3:
                transform.position = destination[3];
                sr.sortingOrder = 2;
                break;
            case 4:
                transform.position = destination[4];
                sr.sortingOrder = 2;
                break;
        }
        if (DestinationCount == 5)
        {
            gameObject.SetActive(false);
            GameObject.Find("UI/AchieveMusic").GetComponent<AchieveDetect>().PassLevel(2);
        }
    }
}
