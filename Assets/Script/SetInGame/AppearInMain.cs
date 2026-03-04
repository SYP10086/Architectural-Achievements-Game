using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppearInMain : MonoBehaviour
{
    GameObject Setting,menu;
    public double time = 1;
    private void Start()
    {
        Setting = GameObject.Find("UI/Setting");
        menu= GameObject.Find("UI/Menu");
        Setting.SetActive(false);
    }
    private void Update()
    {
        if (Setting != null && time <= 0&&time!=-100)
        {
            Setting.SetActive(true);
            time = -100;
        }
        else if (time > 0)
            time -= Time.deltaTime;
    }
}
