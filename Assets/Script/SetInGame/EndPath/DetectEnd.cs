using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectEnd : MonoBehaviour
{
    void Update()
    {
        if(AchieveDetect.achieve[0]&& AchieveDetect.achieve[1]&& AchieveDetect.achieve[2]&& !EndInformation.endOver)
        {
            EndInformation.end = true;
            SceneManager.LoadScene("Galary");
        }
    }
}
