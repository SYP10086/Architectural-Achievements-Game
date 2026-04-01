using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DisappearText : MonoBehaviour
{
    GameObject AC;
    UnityEngine.Color color;
    void Start()
    {
        AC = GameObject.Find("UI/AchieveMusic");
    }
    void Update()
    {
        if(AC.GetComponent<AchieveDetect>().start)
        {
            color = GetComponent<Text>().color;
            color.a-= 1 / (float)(AC.GetComponent<AchieveDetect>().delaytime)*Time.deltaTime;
            GetComponent<Text>().color= color;
        }
    }
}
