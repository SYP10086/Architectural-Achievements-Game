using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveDetect : MonoBehaviour
{
    static public bool[] achieve= {false,false,false };
    private void Awake()
    {
       achieve[0] = (PlayerPrefs.GetInt("achieve[0]", 0)==1);
       achieve[1] = (PlayerPrefs.GetInt("achieve[1]", 0)==1);
       achieve[2] = (PlayerPrefs.GetInt("achieve[2]", 0)==1);
    }
}
