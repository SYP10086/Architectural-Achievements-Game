using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    [SerializeField] static public bool OnShow = false;
    bool action=false;
    GameObject menu;
    private void Awake()
    {
        menu = GameObject.Find("UI/Menu");
    }
    private void Update()
    {
        if (OnShow&&!action)
        {
            menu.SetActive(true);
            action = true;
        }
        else if (!OnShow && !action) 
        { 
            menu.SetActive(false);
            action = true;
        }
    }
    public void PressSetting()
    { 
        OnShow = !OnShow;
        action = false;
    }
}
