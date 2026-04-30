using System.Collections;
using UnityEngine;

public class DestinationMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] destination = new Vector3[5];

    public static int DestinationCount { get; set; } = -1;

    void Start()
    {
        DestinationCount = -1;    
    }

    void Update()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        switch (DestinationCount)
        {
            case -1:
                break;
            case 0:
                transform.position = destination[0];
                sr.sortingOrder = 3;
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
                sr.sortingOrder = 3;
                break;
        }

        if (DestinationCount == 5)
        {
            gameObject.SetActive(false);
            GameObject.Find("UI/AchieveMusic").GetComponent<AchieveDetect>().PassLevel(2);
        }
    }
}