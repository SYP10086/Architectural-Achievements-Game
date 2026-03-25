using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideWhichShow : MonoBehaviour
{
    //AchieveDetect.achieve[1];
    GameObject[] gameObjects = new GameObject[3];
    private void Awake()
    {
        gameObjects[0] = GameObject.Find("Square");
        gameObjects[1] = GameObject.Find("Square (1)");
        gameObjects[2] = GameObject.Find("Square (2)");
    }
    private void Start()
    {
        gameObjects[0].SetActive(!AchieveDetect.achieve[0]);
        gameObjects[1].SetActive(!AchieveDetect.achieve[1]);
        gameObjects[2].SetActive(!AchieveDetect.achieve[2]);
    }
}
