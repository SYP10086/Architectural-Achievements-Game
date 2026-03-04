using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
