using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Cinemachine.DocumentationSortingAttribute;

public class OutherButton : MonoBehaviour
{
    public void GotoGalary()
    {
        SettingButton.OnShow = false;
        SceneManager.LoadScene("Galary");
    }
    public void BackMain()
    {
        SettingButton.OnShow = false;
        SceneManager.LoadScene("Beginning");
    }
    public void Clear()
    {
        AchieveDetect.achieve[0] = false;
        AchieveDetect.achieve[1] = false;
        AchieveDetect.achieve[2] = false;
        EndInformation.endOver=false;
        EndInformation.end=false;
        for (int level=0;level<3;level++)
        {
            PlayerPrefs.SetInt($"achieve[{level}]", AchieveDetect.achieve[level] ? 1 : 0);
        }
    }
}
